<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
  <xsl:output method="xml" indent="yes"/>

  <xsl:template match="/">
    <table >
      <xsl:for-each select="Medicamentos/Medicamento">
        <tr>
          <td style="background-color:#f96e5b;padding:4px;font-size:25pt;font-weight:bold;color:white">
            <xsl:value-of select="NombreMedicamento"/>
          </td>
        </tr>
        <tr>
          <td style="margin-left:20px;margin-botton:1em;font-size:10pt">
            <xsl:value-of select="Descripcion"/>
          </td>
        </tr>
        <tr>
          
        </tr>
        <tr>
          <td style="margin-left:20px;margin-botton:1em;font-size:25pt">
            $<xsl:value-of select="Precio"/>
          </td>
        </tr>
        <tr>

        </tr>
        <tr>
          <td style="margin-left:20px;margin-botton:1em;font-size:10pt">
            Tipo: <xsl:value-of select="Tipo"/>
          </td>
        </tr>
        <tr>
          <td style="margin-left:20px;margin-botton:1em;font-size:10pt">
            Stock Disponible: <xsl:value-of select="Stock"/>
          </td>
        </tr>
        <tr>
          <td style="margin-left:20px;margin-botton:1em;font-size:10pt">
            Codigo: <xsl:value-of select="Codigo"/>
          </td>
        </tr>
        <tr style="background-color:#f96e5b;font-size:10pt;color:white">
          <xsl:for-each select="Farmaceutica">
            <td>
              <xsl:value-of select="NombreFarmaceutica"/>

               - Direccion: <xsl:value-of select="DireccionFisica"/>

               - Telefono: <xsl:value-of select="Telefono"/>

               - Correo: <xsl:value-of select="Correo"/>
            </td>

          </xsl:for-each>
        </tr>
      </xsl:for-each>
    </table>
  </xsl:template>
</xsl:stylesheet>
