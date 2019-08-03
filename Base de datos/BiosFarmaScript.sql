use master 
GO 
 
IF EXISTS (SELECT * FROM Sysdatabases WHERE name = 'BiosFarma') 
BEGIN  
	DROP DATABASE BiosFarma 
END 
GO 
 
CREATE DATABASE BiosFarma 
ON( 
	NAME=BiosFarma, 
	FILENAME='C:\db\BiosFarma.mdf' 
) 
GO 
 
USE BiosFarma 
GO 
 
CREATE TABLE Farmaceuticas 
(nombre varchar(20) NOT NULL PRIMARY KEY,   
direccion_fiscal varchar(50) NOT NULL, 
telefono int NOT NULL CONSTRAINT chk_tel CHECK (telefono not like '%[^0-9]%'), 
correo varchar(50) NOT NULL Check (correo LIKE '%_@_%_.__%'), 
activa bit NOT NULL DEFAULT 1 
) 
GO 
 
 
CREATE TABLE Medicamentos 
(nombre_farma varchar(20) NOT NULL FOREIGN KEY REFERENCES Farmaceuticas(nombre), 
codigo int NOT NULL CONSTRAINT chk_cod CHECK (codigo not like '%[^0-9]%'), 
nombre varchar(20) NOT NULL, 
descripcion varchar(50) NOT NULL, 
precio int NOT NULL CONSTRAINT chk_pre CHECK (precio not like '%[^0-9]%'), 
tipo varchar(15) check (tipo in('cardiologico','diabeticos','otros')) NOT NULL, 
stock int NOT NULL CONSTRAINT chk_stock CHECK (stock not like '%[^0-9]%'), 
activo bit NOT NULL DEFAULT 1, 
PRIMARY KEY(nombre_farma, codigo)  
) 
GO 
 
 
CREATE TABLE Usuarios 
(ci int NOT NULL PRIMARY KEY CONSTRAINT chk_ci CHECK (ci not like '%[^0-9]%' and len(ci) = 8), 
nombre_usuario varchar(20) NOT NULL UNIQUE, 
contrasena char(7) NOT NULL CHECK (contrasena like '%[a-zA-Z]%%[a-zA-Z]%%[a-zA-Z]%%[a-zA-Z]%%[a-zA-Z]%%[0-9]%%[0-9]%' and len(contrasena) = 7), 
nombre_completo varchar(50) NOT NULL 
) 
GO 
 
CREATE TABLE Encargados 
(ci int NOT NULL PRIMARY KEY FOREIGN KEY REFERENCES Usuarios(ci) CONSTRAINT chk_ci2 CHECK (ci not like '%[^0-9]%'), 
telefono int NOT NULL CONSTRAINT chk_tel2 CHECK (telefono not like '%[^0-9]%'), 
) 
GO 
 
CREATE TABLE Empleados 
(ci int NOT NULL PRIMARY KEY FOREIGN KEY REFERENCES Usuarios(ci) CONSTRAINT chk_ci3 CHECK (ci not like '%[^0-9]%'), 
horario_inicio TIME NOT NULL, 
horario_fin TIME NOT NULL, 
activo bit NOT NULL DEFAULT 1 
 
) 
GO 
 
 
CREATE TABLE PedidosCabezal 
(numero int NOT NULL IDENTITY(1,1) PRIMARY KEY, 
fecha_realizado DATETIME NOT NULL default Getdate(), 
direccion_entrega varchar(50) NOT NULL, 
estado varchar(10) check (estado in('GENERADO','ENVIADO','ENTREGADO')) DEFAULT 'GENERADO' NOT NULL, 
ci_empleado int NOT NULL FOREIGN KEY REFERENCES Empleados(ci)CONSTRAINT chk_ci4 CHECK (ci_empleado not like '%[^0-9]%') 
) 
GO 
 
CREATE TABLE PedidosLineas 
(nombre_farma varchar(20) NOT NULL, 
codigo_medicamento int NOT NULL CONSTRAINT chk_med2 CHECK (codigo_medicamento not like '%[^0-9]%'), 
numero int NOT NULL FOREIGN KEY REFERENCES PedidosCabezal(numero), 
cantidad int NOT NULL CONSTRAINT chk_cant2 CHECK (cantidad not like '%[^0-9]%'), 
FOREIGN KEY(nombre_farma,codigo_medicamento) REFERENCES Medicamentos(nombre_farma,codigo), 
PRIMARY KEY(nombre_farma,codigo_medicamento,numero) 
) 
GO 


CREATE TABLE HorasExtras
(ci int NOT NULL FOREIGN KEY REFERENCES Usuarios(ci), 
fecha DATETIME NOT NULL,
minutos int NOT NULL,
PRIMARY KEY (ci,fecha)
)
GO


/*************************************************************************/
CREATE PROCEDURE AgregaHorasExtras 
@ci int,
@fecha DATETIME,
@minutos int
AS
BEGIN

	IF NOT EXISTS (SELECT * FROM Empleados WHERE ci = @ci) 
		RETURN -1 --No existe el empleado 
	
	--Compruebo si existe el registro
	IF NOT EXISTS (SELECT * FROM HorasExtras WHERE ci = @ci AND fecha = @fecha) 
	BEGIN
		INSERT INTO HorasExtras(ci,fecha,minutos) VALUES(@ci,@fecha,@minutos)
		
		IF @@ERROR > 0 
		RETURN -6 --Error en la Base de datos. 
		 
		RETURN 1 --Horas cargadas correctamente.
	END
	
	--Si llego aca es porque ya existe un registro.
	UPDATE HorasExtras SET minutos = @minutos
	WHERE ci = @ci AND fecha = @fecha
	
	IF @@ERROR > 0 
		RETURN -6 --Error en la Base de datos. 
		 
	RETURN 1 --Horas actualizadas correctamente.
	
END
GO 
 
/************************* FARMACEUTICAS *********************************/ 
/*************************************************************************/ 
CREATE PROCEDURE AltaFarmaceutica 
@nombre varchar(20), 
@direccion_fiscal varchar(50), 
@telefono int, 
@correo varchar(50) 
AS 
BEGIN 
	IF EXISTS (SELECT * FROM Farmaceuticas WHERE nombre = @nombre AND activa = 1) 
		RETURN -1 --La Farmaceutica ya existe. 
		 
	IF EXISTS (SELECT * FROM Farmaceuticas WHERE nombre = @nombre AND activa = 0) 
	BEGIN 
		UPDATE Farmaceuticas SET direccion_fiscal = @direccion_fiscal, telefono = @telefono, correo = @correo, activa = 1 
		WHERE nombre = @nombre 
		 
		IF @@ERROR > 0 
			RETURN -6 --Error en la Base de datos. 
			 
		RETURN 1 --Farmaceutica activada correctamente. 
	END 
	 
	--Si llego aca es porque no existe. Hago el Insert. 
	INSERT Farmaceuticas(nombre,direccion_fiscal,telefono,correo) VALUES(@nombre,@direccion_fiscal,@telefono,@correo) 
	IF @@ERROR > 0 
			RETURN -6 --Error en la Base de datos. 
			 
	RETURN 1 --Farmaceutica dada de alta correctamente. 
END 
GO 
 
 
CREATE PROCEDURE ModificarFarmaceutica 
@nombre varchar(20), 
@direccion_fiscal varchar(50), 
@telefono int, 
@correo varchar(50) 
AS 
BEGIN 
	IF NOT EXISTS (SELECT * FROM Farmaceuticas WHERE nombre = @nombre AND activa = 1) 
		RETURN -1 --La Farmaceutica no existe o no esta activa. 
		 
	UPDATE Farmaceuticas SET direccion_fiscal = @direccion_fiscal, telefono = @telefono, correo = @correo 
	WHERE nombre = @nombre 
	 
	IF @@ERROR > 0 
		RETURN -6 --Error en la Base de datos. 
		 
	RETURN 1 --Farmaceutica modificada correctamente. 
 
END 
GO 
 
 
CREATE PROCEDURE EliminarFarmaceutica 
-- Alter procedure EliminarFarmaceutica
@nombre varchar(20) 
AS 
BEGIN 
	IF NOT EXISTS (SELECT * FROM Farmaceuticas WHERE nombre = @nombre) 
		RETURN -1 --La Farmaceutica no existe 
		 
		 --Si existe un pedido activo para esa farmaceutica
	IF EXISTS (SELECT * FROM PedidosLineas WHERE nombre_farma = @nombre) 
	BEGIN 
		UPDATE Farmaceuticas SET activa = 0 
		WHERE nombre = @nombre 
		 
		IF @@ERROR > 0 
			RETURN -6 --Error en la Base de Datos. 
			 
		RETURN 1 --Farmaceutica eliminada correctamente. 
	END 
	 
	--Si llego aca es porque existe y no hay pedidos activos. 
	
	
	--Elimino los Medicamentos de esa farmaceutica. 
	DELETE FROM Medicamentos WHERE nombre_farma = @nombre 
	IF @@ERROR > 0 
	BEGIN 
		ROLLBACK TRAN 
		RETURN -6 --Error en la Base de Datos. 
	END 
	
	BEGIN TRAN 
	--Elimino la Farmaceutica 
	DELETE FROM Farmaceuticas WHERE nombre = @nombre 
	IF @@ERROR > 0 
	BEGIN 
		ROLLBACK TRAN 
		RETURN -6 --Error en la Base de Datos. 
	END 
	 
	
		 
	COMMIT TRAN 
	RETURN 1 --Farmaceutica eliminada correctamente. 
	 
END 
GO 
 
 
Create Procedure BuscarFarmaceuticaActiva @nombre varchar (20) As 
Begin 
	Select * From Farmaceuticas where nombre  = @nombre and activa = 1  
End 
go 
 
 
Create Procedure BuscarFarmaceuticaTodas @nombre varchar (20) As 
Begin 
	Select * From Farmaceuticas where nombre  = @nombre 
End 
go 
 
 
CREATE PROC ListarFarmaceuticasActivas 
--ALTER PROC ListarFarmaceuticasActivas 
as 
BEGIN 
 
SELECT * FROM Farmaceuticas WHERE activa = 1 
 
END 
go 
 
 
CREATE PROC ListarFarmaceuticas 
--ALTER PROC ListarFarmaceuticas 
as 
BEGIN 
 
SELECT * FROM Farmaceuticas  
 
END 
go 
 
 
 
/*************************************************************************/ 
 
/****************************** MEDICAMENTOS *****************************/ 
/*************************************************************************/ 
CREATE PROCEDURE AltaMedicamentos 
@nombre_farma varchar(20), 
@codigo int, 
@nombre varchar(20), 
@descripcion varchar(50), 
@precio int, 
@tipo varchar(15), 
@stock int 
AS 
BEGIN 
	IF EXISTS (SELECT * FROM Medicamentos WHERE nombre_farma = @nombre_farma AND codigo = @codigo AND activo = 1) 
		RETURN -1 --El Medicamento ya existe. 
		 
	IF EXISTS (SELECT * FROM Medicamentos WHERE nombre_farma = @nombre_farma AND codigo = @codigo AND activo = 0) 
	BEGIN 
		UPDATE Medicamentos SET nombre = @nombre, descripcion = @descripcion, precio = @precio, tipo = @tipo, stock = @stock, activo = 1 
		WHERE nombre_farma = @nombre_farma AND codigo = @codigo 
		 
		IF @@ERROR > 0 
			RETURN -6 --Error en la Base de datos. 
			 
		RETURN 1 --Medicamento activado correctamente. 
	END 
	 
	--Si llego aca es porque no existe. Hago el Insert. 
	INSERT Medicamentos(nombre_farma, codigo, nombre, descripcion, precio, tipo, stock) VALUES(@nombre_farma, @codigo, @nombre, @descripcion, @precio, @tipo, @stock) 
	IF @@ERROR > 0 
			RETURN -6 --Error en la Base de datos. 
			 
	RETURN 1 --Medicamento dado de alta correctamente. 
END 
GO 
 
CREATE PROCEDURE ModificarMedicamentos 
@nombre_farma varchar(20), 
@codigo int, 
@nombre varchar(20), 
@descripcion varchar(50), 
@precio int, 
@tipo varchar(15) 
AS 
BEGIN 
	IF NOT EXISTS (SELECT * FROM Medicamentos WHERE nombre_farma = @nombre_farma AND codigo = @codigo) 
		RETURN -1 --El Medicamento no existe 
		 
	UPDATE Medicamentos SET nombre = @nombre, descripcion = @descripcion, precio = @precio, tipo = @tipo 
	WHERE nombre_farma = @nombre_farma AND codigo = @codigo 
	 
	IF @@ERROR > 0 
		RETURN -6 --Error en la Base de datos. 
		 
	RETURN 1 --Medicamento modificado correctamente. 
 
END 
GO 
 
CREATE PROCEDURE EliminarMedicamentos 
@nombre_farma varchar(20), 
@codigo int 
AS 
BEGIN 
	IF NOT EXISTS (SELECT * FROM Medicamentos WHERE nombre_farma = @nombre_farma AND codigo = @codigo AND activo = 1) 
		RETURN -1 --El Medicamento no existe o no esta activo. 
		 
	IF EXISTS (SELECT * FROM PedidosLineas WHERE nombre_farma = @nombre_farma AND codigo_medicamento = @codigo) 
	BEGIN 
		UPDATE Medicamentos SET activo = 0 
		WHERE nombre_farma = @nombre_farma AND codigo = @codigo 
		 
		IF @@ERROR > 0 
			RETURN -6 --Error en la Base de Datos. 
			 
		RETURN 1 --Medicamento eliminado correctamente. 
	END 
	 
	--Si llego aca es porque existe y no hay pedidos activos. 
	DELETE FROM Medicamentos WHERE nombre_farma = @nombre_farma AND codigo = @codigo 
	 
	IF @@ERROR > 0 
		RETURN -6 --Error en la Base de Datos. 
		 
	RETURN 1 --Medicamento eliminado correctamente. 
	 
END 
GO 
 
CREATE PROCEDURE MedicamentosEnStock 
AS 
BEGIN 
	-- SE VA A UTILIZAR EN FORMULARIO WEB, CONSULTA DE MEDICAMENTOS 
	SELECT m.codigo, m.nombre, m.descripcion, m.tipo, m.precio, m.stock, m.nombre_farma 
	FROM Medicamentos m INNER JOIN Farmaceuticas f ON m.nombre_farma = f.nombre 
	WHERE m.stock > 0 AND m.activo = 1 
END  
GO 
 
Create Procedure BuscarMedicamentosActivos @nombre_farma varchar (20), @codigo int As 
Begin 
	Select * From Medicamentos where nombre_farma  = @nombre_farma and codigo = @codigo and activo = 1  
End 
go 
 
Create Procedure BuscarMedicamentosTodos @nombre_farma varchar (20), @codigo int As 
Begin 
	Select * From Medicamentos where nombre_farma  = @nombre_farma and codigo = @codigo 
End 
go 
 
 
CREATE PROC ListarMedicamentosActivos 
--ALTER PROC ListarMedicamentosActivos 
as 
BEGIN 
 
SELECT * FROM Medicamentos WHERE activo = 1 
 
END 
go 
 
 
CREATE PROC ListarMedicamentos 
--ALTER PROC ListarMedicamentos 
as 
BEGIN 
 
SELECT * FROM Medicamentos  
 
END 
go 
 
/*************************************************************************/ 
 
 
 
/*************************************************************************/ 
 
/****************************** ENCARGADOS *****************************/ 
/*************************************************************************/ 
 
 
CREATE PROCEDURE AltaEncargado 
--ALTER PROCEDURE AltaEncargado 
@ci int, 
@nombre_usuario varchar(20), 
@contrasena char(7), 
@nombre_completo varchar(50), 
@telefono int 
AS 
BEGIN 
	IF EXISTS (SELECT * FROM Usuarios WHERE ci = @ci) 
		RETURN -1 --El Usuario ya existe. 
	 
	DECLARE @Sentencia varchar(200) 
	 
	IF EXISTS (SELECT * FROM Encargados WHERE ci = @ci) 
	BEGIN 
		BEGIN TRAN 
			UPDATE Usuarios SET nombre_usuario = @nombre_usuario, contrasena = @contrasena, nombre_completo = @nombre_completo 
			WHERE ci = @ci 
			 
			IF @@ERROR > 0 
				BEGIN 
				ROLLBACK TRAN 
				RETURN -6 --Error en la Base de datos. 
				END 
				 
			UPDATE Encargados SET telefono = @telefono 
			WHERE ci = @ci 
			 
			IF @@ERROR > 0 
				BEGIN 
				ROLLBACK TRAN 
				RETURN -6 --Error en la Base de datos.	 
				END 
				 
			--CREO USUARIO DE SERVIDOR 
			SET @Sentencia = 'CREATE LOGIN [' + @nombre_usuario + '] WITH PASSWORD =' + QUOTENAME(@contrasena, '''') 
			EXEC (@Sentencia) 
			 
			IF (@@ERROR <> 0) 
			BEGIN  
				ROLLBACK TRAN 
				RETURN -2 --Error Crear usuario de Logueo 
			END 
		 
			--CREO USUARIO DE BD 
			SET @Sentencia = 'CREATE USER [' + @nombre_usuario + '] FROM LOGIN [' + @nombre_usuario + ']' 
			EXEC (@Sentencia) 
			 
			IF (@@ERROR <> 0) 
			BEGIN  
				ROLLBACK TRAN 
				RETURN -3 --Error Crear usuario de BD 
			END 
				 
		COMMIT TRAN 
		--ASIGNACION DE ROLES 
		EXEC sp_addsrvrolemember @rolename = 'securityadmin', @loginame = @nombre_usuario 
		--EXEC sp_addrolemember @rolename = 'db_securityadmin', @membername = @nombre_usuario 
		EXEC sp_addrolemember @rolename = 'db_owner', @membername = @nombre_usuario 
 
		EXEC sp_addrolemember @rolename = 'rolEncargados', @membername = @nombre_usuario 
 
		RETURN 1 --Encargado activado correctamente. 
	END 
	 
	BEGIN TRAN 
		INSERT Usuarios(ci, nombre_usuario, contrasena, nombre_completo) VALUES  
		(@ci, @nombre_usuario, @contrasena, @nombre_completo) 
 
		IF @@ERROR<>0 
		BEGIN 
			ROLLBACK TRAN 
			RETURN -6 --ERROR SQL 
		END 
 
		INSERT Encargados(ci, telefono) VALUES (@ci, @telefono) 
 
		IF @@ERROR<>0 
		BEGIN 
			ROLLBACK TRAN 
			RETURN -6 --ERROR SQL 
		END 
 
		--CREO USUARIO DE SERVIDOR 
		SET @Sentencia = 'CREATE LOGIN [' + @nombre_usuario + '] WITH PASSWORD =' + QUOTENAME(@contrasena, '''') 
		EXEC (@Sentencia) 
		 
		IF (@@ERROR <> 0) 
		BEGIN  
			ROLLBACK TRAN 
			RETURN -2 --Error Crear usuario de Logueo 
		END 
	 
		--CREO USUARIO DE BD 
		SET @Sentencia = 'CREATE USER [' + @nombre_usuario + '] FROM LOGIN [' + @nombre_usuario + ']' 
		EXEC (@Sentencia) 
		 
		IF (@@ERROR <> 0) 
		BEGIN  
			ROLLBACK TRAN 
			RETURN -3 --Error Crear usuario de BD 
		END 
 
	COMMIT TRAN 
	 
	--ASIGNACION DE ROLES 
	EXEC sp_addsrvrolemember @rolename = 'securityadmin', @loginame = @nombre_usuario 
	--EXEC sp_addrolemember @rolename = 'db_securityadmin', @membername = @nombre_usuario 
	EXEC sp_addrolemember @rolename = 'db_owner', @membername = @nombre_usuario 
 
	EXEC sp_addrolemember @rolename = 'rolEncargados', @membername = @nombre_usuario 
	 
	 
	RETURN 1						 
 
END	 
GO 
 
 
Create Procedure BuscarEncargados 
--alter Procedure BuscarEncargados  
@ci int 
as 
Begin 
	Select * FROM Usuarios U INNER JOIN Encargados E ON U.ci = E.ci  
	where U.ci = @ci 
End 
go 
 
 
CREATE Procedure ListadoEncargados 
--alter Procedure ListadoEncargados 
As  
Begin 
	Select * FROM Usuarios U INNER JOIN Encargados E ON U.ci = E.ci  
End 
go	 
 
 
 
/*************************************************************************/ 
 
/****************************** CAMBIO DE CONTRASENA *****************************/ 
/*************************************************************************/ 
 
CREATE PROCEDURE CambioContrasena 
--ALTER PROC CambioContrasena 
@nombre_usuario varchar(20), 
@contrasena char(7), 
@new_contrasena char(7) 
AS  
BEGIN 
	 
	IF NOT EXISTS (SELECT * FROM Usuarios WHERE nombre_usuario = @nombre_usuario and contrasena = @contrasena) 
	RETURN -1 -- NO EXISTE USUARIO 
	 
	DECLARE @Sentencia varchar(200) 
	 
	BEGIN TRAN 
		UPDATE Usuarios SET contrasena = @new_contrasena 
		WHERE nombre_usuario = @nombre_usuario 
		 
		IF @@ERROR > 0 
		BEGIN 
			ROLLBACK TRAN 
			RETURN -6 --Error en la Base de Datos. 
		END 
		 
		--Modifico usuario de inicio de Sesion. 
		/* 
		SET @Sentencia = 'ALTER LOGIN [' + @nombre_usuario + '] WITH PASSWORD =' + QUOTENAME(@new_contrasena, '''') 
		EXEC (@Sentencia) 
		 
		 
		IF @@ERROR > 0 
		BEGIN 
			ROLLBACK TRAN 
			RETURN -2 --Error al Actualizar el inicio de sesion.	 
		END 
		*/ 
		 
	COMMIT TRAN		 
	 
	--Modifico usuario de inicio de Sesion. 
	EXEC sp_password @contrasena, @new_contrasena, @nombre_usuario 
	 
	RETURN 1 --Conrasena reiniciada correctamente. 
	 
END 
GO 
 
 
/*************************************************************************/ 
 
/****************************** LOGIN *****************************/ 
/*************************************************************************/ 
 
 
CREATE PROCEDURE LoginEmpleado 
--ALTER PROC LoginEmpleado 
@nombre_usuario char(20), 
@contrasena char(7)  
AS  
BEGIN 
	SELECT E.ci 
	, U.contrasena 
	, U.nombre_usuario 
	, U.nombre_completo 
	, E.horario_inicio 
	, E.horario_fin FROM EMPLEADOS E INNER JOIN Usuarios U on E.ci = U.ci WHERE U.nombre_usuario = @nombre_usuario AND CONTRASENA = @CONTRASENA  
END 
GO 
 
 
 
CREATE PROCEDURE LoginEncargado 
--ALTER PROC LoginEncargado 
@nombre_usuario char(20), 
@contrasena char(7)  
AS  
BEGIN 
	SELECT  
	E.ci 
	, U.contrasena 
	, U.nombre_usuario 
	, U.nombre_completo 
	, E.telefono 
	FROM ENCARGADOS E INNER JOIN Usuarios U on E.ci = U.ci WHERE U.nombre_usuario = @nombre_usuario AND CONTRASENA = @CONTRASENA  
END 
GO 
 
 
/*************************************************************************/ 
 
/********************************* PEDIDOS *******************************/ 
/*************************************************************************/ 
CREATE PROCEDURE AltaPedidoCabezal 
@direccion_entrega varchar(50), 
@ci_empleado int 
AS 
BEGIN  
	IF NOT EXISTS (SELECT * FROM Empleados WHERE ci = @ci_empleado AND activo = 1) 
	RETURN -1 --El empleado no existe. 
	 
	INSERT INTO PedidosCabezal(direccion_entrega,ci_empleado) VALUES (@direccion_entrega, @ci_empleado) 
	 
	IF @@ERROR > 0 
		RETURN -6 --Error en la Base de Datos. 
		 
	--RETORNO EL IDENTITY PARA SABER QUE NUMERO DE PEDIDO FUE ASIGNADO. 
	RETURN @@IDENTITY 
END 
GO 
 
CREATE PROCEDURE AltaPedidoLineas 
@nombre_farma varchar(20), 
@codigo_medicamento int, 
@numero int, 
@cantidad int 
AS 
BEGIN  
	IF NOT EXISTS (SELECT * FROM PedidosCabezal WHERE numero = @numero) 
	RETURN -1 --No existe un pedido con ese numero. 
	 
	DECLARE @stock_actual int 
	 
	SELECT @stock_actual = stock FROM Medicamentos WHERE nombre_farma = @nombre_farma AND codigo = @codigo_medicamento AND activo = 1 
	 
	IF (@stock_actual is null) 
	RETURN -2 --El Medicamento no Existe 
	 
	IF (@stock_actual < @cantidad) 
	RETURN -3 --No hay Stock suficiente para ese medicamento 
	 
	-- No lleva transaccion porque se hace dentro de la persistencia. 
	INSERT INTO PedidosLineas(nombre_farma, codigo_medicamento, numero, cantidad) VALUES(@nombre_farma, @codigo_medicamento, @numero, @cantidad) 
 
	IF @@ERROR > 0 
	BEGIN 
		RETURN -6 --ERROR EN LA BASE DE DATOS. 
	END	 
	 
	UPDATE Medicamentos SET stock = stock - @cantidad 
	WHERE nombre_farma = @nombre_farma AND codigo = @codigo_medicamento 
	 
	IF @@ERROR > 0 
	BEGIN 
		RETURN -6 --ERROR EN LA BASE DE DATOS. 
	END	 
 
	RETURN 1 
END 
GO 
 
 
--(estado in('GENERADO','ENVIADO','ENTREGADO')) DEFAULT 'GENERADO' NOT NULL, 
 
 
CREATE PROCEDURE CambioEstado 
@numero int, 
@estadoNuevo varchar(10) 
AS 
BEGIN 
 
	--Compruebo si existe el pedido y guardo su estado 
	DECLARE @estadoAnterior varchar(10) 
	select @estadoAnterior = estado FROM PedidosCabezal WHERE numero = @numero 
	 
	--Si el estado es NULL es porque la consulta de arriba no obtuvo resultados. 
	IF (@estadoAnterior is null) 
	RETURN -1 --No existe un pedido con ese numero. 
	 
	if (@estadoAnterior = 'GENERADO' and @estadoNuevo != 'ENVIADO') 
		RETURN -2 --El estado luego de GENERADO deberia ser ENVIADO 
 
	if (@estadoAnterior = 'ENVIADO' and @estadoNuevo != 'ENTREGADO') 
	RETURN -3 --El estado luego de ENVIADO deberia ser ENTREGADO 
	 
	UPDATE PedidosCabezal SET estado = @estadoNuevo 
	WHERE numero = @numero 
	 
	IF @@ERROR > 0  
	RETURN -6 --ERROR EN LA BASE DE DATOS 
	 
	RETURN 1 
END 
GO 
 
CREATE PROCEDURE ListaPedidosGeneradoEnviado 
AS 
BEGIN  
	SELECT pc.numero 
	, pc.fecha_realizado 
	, pc.direccion_entrega 
	, pc.estado 
	, pc.ci_empleado 
	FROM PedidosCabezal pc  
	WHERE pc.estado IN('GENERADO','ENVIADO') 
END 
GO 
 
CREATE PROCEDURE ListaPedidosEsteAno 
AS 
BEGIN  
	SELECT pc.numero 
	, pc.fecha_realizado 
	, pc.direccion_entrega 
	, pc.estado 
	, pc.ci_empleado 
	FROM PedidosCabezal pc  
	WHERE year(pc.fecha_realizado) = YEAR(getdate()) 
END 
GO 
 
CREATE PROCEDURE BuscarPedido 
@numero int 
AS 
BEGIN  
	SELECT pc.numero 
	, pc.fecha_realizado 
	, pc.direccion_entrega 
	, pc.estado 
	, pc.ci_empleado 
	FROM PedidosCabezal pc  
	WHERE pc.numero = @numero 
END 
GO 
 
CREATE PROCEDURE LineasDeUnPedido 
@numero int 
AS 
BEGIN 
	SELECT pl.codigo_medicamento 
	, pl.cantidad 
	, pl.nombre_farma 
	FROM PedidosCabezal pc INNER JOIN PedidosLineas pl ON pc.numero = pl.numero 
	WHERE pc.numero = @numero 
END 
GO 
/*************************************************************************/ 
 
 
/********************************* EMPLEADOS *******************************/ 
/*************************************************************************/ 
 
 
CREATE PROCEDURE AltaEmpleado 
--ALTER PROCEDURE AltaEmpleado 
@ci int, 
@nombre_usuario varchar(20), 
@contrasena char(7), 
@nombre_completo varchar(50), 
@horario_inicio TIME, 
@horario_fin TIME  
AS 
BEGIN 
	IF EXISTS (SELECT * FROM Empleados WHERE ci = @ci and activo = 1) 
		RETURN -1 --El Empleado ya existe. 
	 
	DECLARE @Sentencia varchar(200) 
	 
	IF EXISTS (SELECT * FROM Empleados WHERE ci = @ci) 
	BEGIN 
		BEGIN TRAN 
			UPDATE Usuarios SET nombre_usuario = @nombre_usuario, contrasena = @contrasena, nombre_completo = @nombre_completo 
			WHERE ci = @ci 
			 
			IF @@ERROR > 0 
			BEGIN 
				ROLLBACK TRAN 
				RETURN -6 --Error en la Base de datos. 
			END 
				 
			UPDATE Empleados SET horario_inicio = @horario_inicio, horario_fin = @horario_fin, activo = 1 
			WHERE ci = @ci 
			 
			IF @@ERROR > 0 
			BEGIN 
				ROLLBACK TRAN 
				RETURN -6 --Error en la Base de datos.	 
			END 
				 
			--CREO USUARIO DE SERVIDOR 
			SET @Sentencia = 'CREATE LOGIN [' + @nombre_usuario + '] WITH PASSWORD =' + QUOTENAME(@contrasena, '''') 
			EXEC (@Sentencia) 
			 
			IF (@@ERROR <> 0) 
			BEGIN  
				ROLLBACK TRAN 
				RETURN -2 --Error Crear usuario de Logueo 
			END 
		 
			--CREO USUARIO DE BD 
			SET @Sentencia = 'CREATE USER [' + @nombre_usuario + '] FROM LOGIN [' + @nombre_usuario + ']' 
			EXEC (@Sentencia) 
			 
			IF (@@ERROR <> 0) 
			BEGIN  
				ROLLBACK TRAN 
				RETURN -3 --Error Crear usuario de BD 
			END 
			 
		COMMIT TRAN 
		 
		--ASIGNACION DE ROLES 
		EXEC sp_addsrvrolemember @rolename = 'securityadmin', @loginame = @nombre_usuario 
		--EXEC sp_addrolemember @rolename = 'db_securityadmin', @membername = @nombre_usuario 
		EXEC sp_addrolemember @rolename = 'rolEmpleados', @membername = @nombre_usuario 
		 
		RETURN 1 --Empleado activado correctamente. 
		END 
	 
	BEGIN TRAN 
		INSERT Usuarios(ci, nombre_usuario, contrasena, nombre_completo) VALUES  
		(@ci, @nombre_usuario, @contrasena, @nombre_completo) 
 
		IF @@ERROR<>0 
		BEGIN 
			ROLLBACK TRAN 
			RETURN -6 --ERROR SQL 
		END 
 
		INSERT Empleados(ci, horario_inicio, horario_fin) VALUES (@ci, @horario_inicio, @horario_fin) 
 
		IF @@ERROR<>0 
		BEGIN 
			ROLLBACK TRAN 
			RETURN -6 --ERROR SQL 
		END 
		 
		--CREO USUARIO DE SERVIDOR 
		SET @Sentencia = 'CREATE LOGIN [' + @nombre_usuario + '] WITH PASSWORD =' + QUOTENAME(@contrasena, '''') 
		EXEC (@Sentencia) 
		 
		IF (@@ERROR <> 0) 
		BEGIN  
			ROLLBACK TRAN 
			RETURN -2 --Error Crear usuario de Logueo 
		END 
	 
		--CREO USUARIO DE BD 
		SET @Sentencia = 'CREATE USER [' + @nombre_usuario + '] FROM LOGIN [' + @nombre_usuario + ']' 
		EXEC (@Sentencia) 
		 
		IF (@@ERROR <> 0) 
		BEGIN  
			ROLLBACK TRAN 
			RETURN -3 --Error Crear usuario de BD 
		END 
 
	COMMIT TRAN 
	 
	--ASIGNACION DE ROLES 
	EXEC sp_addsrvrolemember @rolename = 'securityadmin', @loginame = @nombre_usuario 
	--EXEC sp_addrolemember @rolename = 'db_securityadmin', @membername = @nombre_usuario 
	EXEC sp_addrolemember @rolename = 'rolEmpleados', @membername = @nombre_usuario 
	 
	RETURN 1						 
 
END	 
GO 
 
CREATE PROC EliminarEmpleado 
--ALTER PROC EliminarEmpleado 
@ci int, 
@nombre_usuario varchar(20) 
as 
BEGIN 
 
	IF NOT EXISTS (SELECT * FROM Empleados WHERE ci = @ci) 
	RETURN -1 --No existe el empleado 
 
--Si existe y tiene pedidos 
	if (Exists(Select * From PedidosCabezal Where ci_empleado = @ci)) 
		Begin 
			DECLARE @Sentencia varchar(200) 
			BEGIN TRAN 
				update Empleados set Activo = 0 where ci = @ci 
				 
				IF (@@ERROR <> 0) 
				BEGIN 
					ROLLBACK TRAN 
					RETURN -6 -- Error SQL 
				END 
					 
				SET @Sentencia = 'DROP USER [' + @nombre_usuario + ']' 
				EXEC (@Sentencia) 
				IF (@@ERROR <> 0) 
				BEGIN 
					ROLLBACK TRAN 
					RETURN -2 -- Error al borrar usuario de BD 
				END 
				 
				SET @Sentencia = 'DROP LOGIN [' + @nombre_usuario + ']' 
				EXEC (@Sentencia) 
				IF (@@ERROR <> 0) 
				BEGIN 
					ROLLBACK TRAN 
					RETURN -3 -- Error al borrar usuario de Logueo 
				END 
				 
			COMMIT TRAN	 
			RETURN 1 --Baja logica, tiene un pedido activo 
		end 
		 
 
	BEGIN TRAN 
		DELETE FROM Empleados WHERE ci = @ci 
 
		IF @@ERROR <> 0  
		BEGIN 
			ROLLBACK TRAN 
			RETURN -6 --ERROR SQL 
		END 
 
		DELETE FROM Usuarios WHERE ci = @ci 
 
		IF @@ERROR <> 0  
		BEGIN 
			ROLLBACK TRAN 
			RETURN -6 --ERROR SQL 
		END 
		 
		SET @Sentencia = 'DROP USER [' + @nombre_usuario + ']' 
		EXEC (@Sentencia) 
		IF (@@ERROR <> 0) 
		BEGIN 
			ROLLBACK TRAN 
			RETURN -2 -- Error al borrar usuario de BD 
		END 
		 
		SET @Sentencia = 'DROP LOGIN [' + @nombre_usuario + ']' 
		EXEC (@Sentencia) 
		IF (@@ERROR <> 0) 
		BEGIN 
			ROLLBACK TRAN 
			RETURN -3 -- Error al borrar usuario de Logueo 
		END 
 
	COMMIT TRAN 
	RETURN 1 
 
END 
GO 
 
 
Create Procedure ModificarEmpleado  
--ALTER PROC ModificarEmpleado  
@ci int, 
@nombre_usuario varchar(20), 
@contrasena char(7), 
@nombre_completo varchar(50), 
@horario_inicio TIME, 
@horario_fin TIME   
AS 
Begin 
         
	if (Not Exists(Select * From Empleados Where ci = @ci and Activo = 1)) 
	return -1 --No existe y no esta activo 
			 
	DECLARE @Sentencia varchar(200) 
	 
	BEGIN TRAN 
		UPDATE Usuarios SET nombre_usuario = @nombre_usuario, contrasena = @contrasena,  
		nombre_completo = @nombre_completo 
		WHERE ci = @ci 
		 
		IF @@ERROR > 0 
		BEGIN 
			ROLLBACK TRAN 
			RETURN -6 --Error en la Base de datos. 
		END 
			 
		UPDATE Empleados SET horario_inicio = @horario_inicio, horario_fin = @horario_fin, activo = 1 
		WHERE ci = @ci 
		 
		IF @@ERROR > 0 
		BEGIN 
			ROLLBACK TRAN 
			RETURN -6 --Error en la Base de datos.	 
		END 
			 
		--Modifico usuario de inicio de Sesion. 
		SET @Sentencia = 'ALTER LOGIN [' + @nombre_usuario + '] WITH PASSWORD =' + QUOTENAME(@contrasena, '''') 
		EXEC (@Sentencia) 
		 
		IF @@ERROR > 0 
		BEGIN 
			ROLLBACK TRAN 
			RETURN -2 --Error al Actualizar el inicio de sesion.	 
		END 
		 
	COMMIT TRAN 
	RETURN 1 --Empleado modificado correctamente.	 
		 
End 
go 
 
 
Create Procedure BuscarEmpleadosActivos @ci int As 
Begin 
	--Devuelvo cualquier cosa en la contrasena por seguridad.
	Select u.ci, u.contrasena, u.nombre_completo, u.nombre_usuario, e.horario_inicio, e.horario_fin From Empleados e INNER JOIN Usuarios u ON e.ci = u.ci where e.ci  = @ci and e.activo = 1  
End 
go 
 
Create Procedure BuscarEmpleadosTodos @ci int As 
Begin 
	--Devuelvo cualquier cosa en la contrasena por seguridad.
	Select u.ci, u.contrasena, u.nombre_completo, u.nombre_usuario, e.horario_inicio, e.horario_fin From Empleados e INNER JOIN Usuarios u ON e.ci = u.ci where e.ci = @ci 
End 
go 
 
 
CREATE Procedure ListadoEmpleados 
--alter Procedure ListadoEmpleados 
As  
Begin 
	Select * FROM Usuarios U INNER JOIN Empleados E ON U.ci = E.ci  
End 
go	 
 
 
CREATE Procedure ListadoEmpleadosActivos 
--alter Procedure ListadoEmpleadosActivos 
As  
Begin 
	Select * FROM Usuarios U INNER JOIN Empleados E ON U.ci = E.ci where activo = 1  
End 
go	 
 
 
CREATE ROLE rolEncargados 
/*Usuarios*/ 
GRANT EXECUTE ON CambioContrasena TO [rolEncargados] 
GRANT EXECUTE ON LoginEncargado TO [rolEncargados] 
/*Encargado*/ 
GRANT EXECUTE ON AltaEncargado TO [rolEncargados] 
/*ABM Empleado*/ 
GRANT EXECUTE ON AltaEmpleado TO [rolEncargados] 
GRANT EXECUTE ON ModificarEmpleado TO [rolEncargados] 
GRANT EXECUTE ON EliminarEmpleado TO [rolEncargados] 
/*ABM Farmaceutica*/ 
GRANT EXECUTE ON AltaFarmaceutica TO [rolEncargados] 
GRANT EXECUTE ON ModificarFarmaceutica TO [rolEncargados] 
GRANT EXECUTE ON EliminarFarmaceutica TO [rolEncargados] 
/*ABM Medicamentos*/ 
GRANT EXECUTE ON AltaMedicamentos TO [rolEncargados] 
GRANT EXECUTE ON ModificarMedicamentos TO [rolEncargados] 
GRANT EXECUTE ON EliminarMedicamentos TO [rolEncargados] 
/*Pedidos*/ 
GRANT EXECUTE ON CambioEstado TO [rolEncargados] 
GRANT EXECUTE ON ListaPedidosEsteAno TO [rolEncargados] 
GRANT EXECUTE ON ListaPedidosGeneradoEnviado TO [rolEncargados] 
 
 
CREATE ROLE rolEmpleados 
/*Usuarios*/ 
GRANT EXECUTE ON CambioContrasena TO [rolEmpleados] 
GRANT EXECUTE ON LoginEmpleado TO [rolEmpleados] 
/*Pedidos*/ 
GRANT EXECUTE ON AltaPedidoCabezal TO [rolEmpleados] 
GRANT EXECUTE ON AltaPedidoLineas TO [rolEmpleados] 
GRANT EXECUTE ON BuscarPedido TO [rolEmpleados] 
GRANT EXECUTE ON LineasDeUnPedido TO [rolEmpleados] 
GRANT EXECUTE ON MedicamentosEnStock TO [rolEmpleados] 
GRANT EXECUTE ON BuscarMedicamentosActivos TO [rolEmpleados]
GRANT EXECUTE ON BuscarFarmaceuticaActiva TO [rolEmpleados]
 
 
 
/***** CARGA INICAL ******/ 
EXEC AltaFarmaceutica 'Urufarma','Juan R. Gomez 3265 Montevideo, Uruguay.',24872748,'contacto@urufarma.com.uy' 
EXEC AltaFarmaceutica 'ION','Paysandu 1023 - Montevideo, Uruguay',29003973,'info@laboratorioion.com' 
EXEC AltaFarmaceutica 'Gramon Bago','Av. Joaquin Suarez 3359 - 11700, Montevideo, Uruguay.',08001856,'info@gramonbago.com.uy' 
EXEC AltaFarmaceutica 'Bayer','Luis Alberto de Herrera 1248 WTC Torre 3 Piso 20 - Unidad 2074/2075 11300 Montevideo - Uruguay',21922300,'comunicaciones@bayer.com' 
EXEC AltaFarmaceutica 'Lazar','Bvar. Artigas 1158 C.P. 11300 Uruguay - Montevideo',27088494,'infoweb@lazar.com.uy' 
EXEC AltaFarmaceutica 'Roemmers','Cno. Maldonado 5634, 12200 Montevideo',25130505,'info@roemmers.com.uy' 
EXEC AltaFarmaceutica 'Caillon & Hamonet','Avda. Italia 5630 Montevideo - Uruguay',26013037,'caillon@caillon.com.uy' 
 
EXEC AltaMedicamentos 'Urufarma',1,'Ibupirac','Alivio sintomotico de: Dolores musculares, Dolores de espalda, Fiebre Dolores de cabeza, Dolor asociado a los estados gripales, Dolores menstruales, Dolores de dientes, Dolores producidos por artritis.',100,'otros',150 
 
EXEC AltaMedicamentos 'ION',1,'Omeprazol','Capsulas con microgranulos gastrorresistentes.',200,'otros',76 
EXEC AltaMedicamentos 'ION',2,'Loratadina','Antialergico antihistaminico.',96,'otros',13 
 
EXEC AltaMedicamentos 'Gramon Bago',1,'Bio Grip C Plus','Antigripal con la formula mas completa, rapida y efectiva.',129,'otros',526 
 
EXEC AltaMedicamentos 'Bayer',1,'Aspirina','Analgesico, antiinflamatorio, antipiretico.',115,'otros',1055 
EXEC AltaMedicamentos 'Bayer',2,'Cardioaspirina','Antiagregante Plaquetario.',150,'cardiologico',25 
 
EXEC AltaMedicamentos 'Lazar',1,'Atenolan','Antihipertensivo - Cardiopatia Isquimica.',590,'cardiologico',19 
 
EXEC AltaMedicamentos 'Roemmers',1,'Diaformina','Diabetes no insulinodependiente.',499,'diabeticos',7 
EXEC AltaMedicamentos 'Roemmers',2,'Lanoxin','Antiarratmico.',345,'cardiologico',33 
EXEC AltaMedicamentos 'Roemmers',3,'Atlansil','Antiarratmico.',285,'cardiologico',8 
 
EXEC AltaMedicamentos 'Caillon & Hamonet',1,'Glimida','Hipoglucemiante oral.',475,'diabeticos',4 
 
EXEC AltaEmpleado 54025652, 'DamianB', 'abcde77','Damian Boz', '20190118 09:00:00','20190118 17:00:00' 
EXEC AltaEmpleado 47777278, 'BrunoB', 'abcde88','Bruno Bardesio', '20190118 09:00:00','20190118 17:00:00' 
EXEC AltaEmpleado 48524325, 'DamianH', 'abcde99','Damian Hernandez', '20190118 09:00:00','20190118 17:00:00' 
EXEC AltaEmpleado 12345678, 'Manolo', 'abcde10','Manolo Nocturno', '20190118 20:00:00','20190119 04:00:00' 
EXEC AltaEmpleado 42345679, 'usu1', 'abcde99','Usuario Empleado', '20190118 10:00:00','20190118 18:00:00' 
EXEC AltaEmpleado 42345670, 'usu2', 'abcde99','Usuario Empleado2', '20190118 18:00:00','20190119 03:00:00' 

EXEC AltaEncargado 48524324,'pepe','abcde12','Pepe Trueno',12345678 
EXEC AltaEncargado 12345677,'Juan','juanm12','Juan Martinez',23051464
EXEC AltaEncargado 12345679,'Julia','julia12','Julia Gonzalez',43021122 

Exec AltaPedidoCabezal 'yaguaron 1414', '42345679' 
EXEC AltaPedidoLineas 'Roemmers',1, 1, 2 

Exec AltaPedidoCabezal 'direccion en algun lado', '42345670' 
EXEC AltaPedidoLineas 'Roemmers',1, 2, 4 
EXEC AltaPedidoLineas 'Roemmers',2, 2, 3

Exec AltaPedidoCabezal 'Roger Balet esq Rambla', '48524325' 
EXEC AltaPedidoLineas 'ION',2, 3, 5 
EXEC AltaPedidoLineas 'Gramon Bago',1, 3, 2
 
Exec AltaPedidoCabezal 'Av Artigas y otra calle', '12345678' 
EXEC AltaPedidoLineas 'Caillon & Hamonet',1, 4, 1 
EXEC AltaPedidoLineas 'Gramon Bago',1, 4, 10
EXEC AltaPedidoLineas 'Urufarma',1, 4, 6
EXEC AltaPedidoLineas 'Roemmers',3, 4, 2
EXEC AltaPedidoLineas 'Bayer',1, 4, 15
 
Exec AltaPedidoCabezal 'Av Brasil 1234', '48524325' 
EXEC AltaPedidoLineas 'Roemmers',1, 5, 1 
EXEC AltaPedidoLineas 'Roemmers',2, 5, 1
EXEC AltaPedidoLineas 'Roemmers',3, 5, 1

Exec AltaPedidoCabezal 'Av Italia 1234', '54025652' 
EXEC AltaPedidoLineas 'Bayer',1, 6, 2
EXEC AltaPedidoLineas 'Bayer',2, 6, 2
EXEC AltaPedidoLineas 'Lazar',1, 6, 3

Exec AltaPedidoCabezal 'Av Italia 1234', '47777278' 
EXEC AltaPedidoLineas 'Urufarma',1, 7, 3
EXEC AltaPedidoLineas 'ION',1, 7, 2
 
USE master
GO

CREATE LOGIN [IIS APPPOOL\DefaultAppPool] FROM WINDOWS 
GO

USE BiosFarma
GO

CREATE USER [IIS APPPOOL\DefaultAppPool] FOR LOGIN [IIS APPPOOL\DefaultAppPool]
GO

Grant Execute to [IIS APPPOOL\DefaultAppPool]
go




/*************************************************************************/ 
 
/*************************************************************************/ 
/* 
SELECT * FROM USUARIOS 
SELECT * FROM EMPLEADOS 
SELECT * FROM Farmaceuticas 
SELECT * FROM Medicamentos 
SELECT * FROM PedidosCabezal 
SELECT * FROM PedidosLineas 

select * from PedidosCabezal join PedidosLineas on PedidosCabezal.numero = PedidosLineas.numero

DECLARE @retorno int 
EXEC @retorno = AltaPedidoCabezal 'yaguaron 1414', '42345679' 
PRINT @retorno 
 
DECLARE @retorno int 
EXEC @retorno =  LineasDeUnPedido 1 
PRINT @retorno 
 
DECLARE @retorno int 
EXEC @retorno =  BuscarPedido 1 
PRINT @retorno 
 
SELECT * FROM PedidosCabezal 
 
DECLARE @retorno int 
EXEC @retorno = AltaEncargado 48524325,'pepe','abcde12','Pepe Trueno',12345678 
PRINT @retorno 
 
DECLARE @retorno int 
EXEC @retorno = AltaEncargado 41231231,'jose','abcde12','Jose Perez',12345678 
PRINT @retorno 
 
 
DECLARE @retorno int 
EXEC @retorno = CambioContrasena 'pepe','abcde12','abcde11'  
PRINT @retorno 
 
DECLARE @retorno int 
EXEC @retorno = CambioContrasena 'pepe','abcde11','abcde13'  
PRINT @retorno 
 
 
DECLARE @retorno int 
EXEC @retorno = AltaEmpleado 42345678, 'usu', 'abcde99','Usuario Empleado', '20190118 10:00:00','20190118 18:00:00' 
PRINT @retorno 
 
DECLARE @retorno int 
EXEC @retorno = CambioContrasena 'usu','abcde99','abcde88'  
PRINT @retorno 
 
DECLARE @retorno int 
EXEC @retorno = ModificarEmpleado 42345678, 'usua', 'abcde00','Usuario Empleado Modificado', '20190118 11:00:00','20190118 19:00:00' 
PRINT @retorno 
 
DECLARE @retorno int 
EXEC @retorno = EliminarEmpleado 42345678 
PRINT @retorno 
 
 
SELECT * FROM Empleados 
SELECT * FROM Encargados 
SELECT * FROM Usuarios 
 
DELETE ENCARGADOS 
DELETE Usuarios 
DROP USER [manolo]
GO
DROP LOGIN [manolo]
GO
DROP USER [pepe] 
GO 
DROP LOGIN [pepe] 
GO 
 
DROP USER [jose] 
GO 
DROP LOGIN [jose] 
GO 
 
DROP USER [usu1] 
GO 
DROP LOGIN [usu1] 
GO 

DROP USER [usu2] 
GO 
DROP LOGIN [usu2] 
*/ 

/*
EXEC AltaEncargado 48524325,'pepe','abcde12','Pepe Trueno',12345678 

exec LoginEncargado 'pepe','abcde12'
exec LoginEmpleado 'Batman', 'ABCDE12'

select * from usuarios
select * from Encargados
delete from Encargados where ci = 48524325
delete from Usuarios where ci = 48524325
select * from Medicamentos where tipo = 'diabeticos'

DECLARE @retorno int 
EXEC @retorno = AgregaHorasExtras 42345679, '20190309', 3
PRINT @retorno 

SELECT * FROM HorasExtras
*/

