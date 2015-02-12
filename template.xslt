<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>

  <xsl:output method="html"/>
  <xsl:param name="UserName"/>


  <xsl:template match="User">
    <tr>
      <td width="250">        
        <xsl:value-of select="UserName"/>
      </td>
      <td>     
        <xsl:value-of select="Age"/>      
      </td>
    </tr>
  </xsl:template>
   

    <xsl:template match="/">

      <html lang="en">

        <body>

          <xsl:text>User name is: </xsl:text>

          <xsl:value-of select="$UserName"/>

          <table bordercolor="#000000" border="1" cellspacing="0" >
            <tr>
              <td width="250" style="font-weight: bold; background-color: silver">
                <center>姓名</center>
              </td>
              <td style="font-weight: bold; background-color: silver">
                <center>年龄</center>
              </td>
            </tr>
            <xsl:apply-templates select="/root/User" />
          </table>

        </body>

      </html>

    </xsl:template>

</xsl:stylesheet>
