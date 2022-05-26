<?xml version="1.0" encoding="utf-8"?>

<xsl:stylesheet version="1.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

	<xsl:template match="/">
		<html>
			<body>
        <table border="2">
          <tr style="background-color:#CC0000;font-size:small;font-family:Arial" >
            <td colspan="15">
              <font size="3" color="white">
                <b>Q1:Overall, how satisfied were you with the service you received?</b>
              </font>
            </td>
          </tr>
          <tr style="background-color:#CC0000;font-size:small;font-family:Arial" >
            <td colspan="15">
              <font size="3" color="white">
                <b>Q2:Rate us on Resolution on Time.</b>
              </font>
            </td>
          </tr>
          <tr style="background-color:#CC0000;font-size:small;font-family:Arial" >
            <td colspan="15">
              <font size="3" color="white">
                <b>Q3:Rate us on Quality of Resolution.</b>
              </font>
            </td>
          </tr>
          <tr></tr>
         </table>
				<table border="2">
					<tr style="background-color:#4169E1;font-size:small;font-family:Arial;color:White">
						<th>Employee Code</th>
						<th>Employee Name</th>
						<th>Email ID</th>
						<th>Mobile No</th>
            <th>Branch Name</th>
            <th>SM Code</th>
            <th>SM Name</th>
						<th>Feedback Date</th>
						<th>Q1</th>
            <th>Q2</th>
            <th>Q3</th>
						<th>Remarks</th>
						
						</tr>
					<xsl:for-each select="NewDataSet/Table">
						<tr>
							<td><xsl:value-of select="AgentCode"/></td>
							<td><xsl:value-of select="AgentName"/></td>
							
							<td><xsl:value-of select="EmailId"/></td>
							<td><xsl:value-of select="MobileNo"/></td>

							
							<td><xsl:value-of select="BranchName"/></td>
              <td>
                <xsl:value-of select="SMCode"/>
              </td>
              <td>
                <xsl:value-of select="SmName"/>
              </td>
							
							<td><xsl:value-of select="FeedbackDate"/></td>
							<td><xsl:value-of select="Q1"/></td>
              <td>
                <xsl:value-of select="Q2"/>
              </td>
              <td>
                <xsl:value-of select="Q3"/>
              </td>
							
							<td><xsl:value-of select="Remarks"/></td>
						
						</tr>
					</xsl:for-each>
				</table>
			</body>
		</html>
	</xsl:template>

</xsl:stylesheet>


