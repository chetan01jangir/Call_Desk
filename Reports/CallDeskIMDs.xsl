<?xml version="1.0" encoding="utf-8"?>

<xsl:stylesheet version="1.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

	<xsl:template match="/">
		<html>
			<body>
				<table border="2">
					<tr style="background-color:#CC0000;font-size:small;font-family:Arial" >
						<td colspan="24">
							<font size="5" color="white">
								<b>Call Desk Report</b>
							</font>
						</td>
					</tr>
					<tr style="background-color:#CC0000;font-size:small;font-family:Arial" >
						<td>
							<font size="2" color="white">
								<b>As on Date : </b>
								<xsl:value-of select="NewDataSet/Table1/Asoandate"/>
							</font>
						</td >
						<td >
							<font size="2" color="white">
								<b>From : </b>
								<xsl:value-of select="NewDataSet/Table1/FromDate"/>
							</font>
						</td >
						<td>
							<font size="2" color="white">
								<b>To : </b>
								<xsl:value-of select="NewDataSet/Table1/ToDate"/>
							</font>
						</td >

						<td colspan="21" >
						</td>
					</tr>
					<!--<tr style="background-color:#CC0000;font-size:small;font-family:Arial" >
						<td>
							<font size="2" color="white">
								<b>RegionID : </b>
								<xsl:value-of select="NewDataSet/Table2/RegionCode"/>
							</font>
						</td >
						<td>
							<font size="2" color="white">
								<b>Region : </b>
								<xsl:value-of select="NewDataSet/Table2/RegionName"/>
							</font>
						</td >
						<td colspan="18" >
						</td>
					</tr>-->				
				</table>
				<table border="2">
					<tr style="background-color:#4169E1;font-size:small;font-family:Arial;color:White">
						<th>Ticket Number</th>
						<th>Call Registered by</th>
						<th>Agent Code</th>
						<th>Branch Name</th>
						<th>Region Name</th>
						<th>Zone Name</th>
						<th>Sales Manager Name</th>
						<th>Sales Manager Code</th>
						<th>Application</th>
						<th>Issue / Request Type</th>
						<th>Issue / Request Sub Type</th>
						<th>Call Date</th>
						<th>User Remark</th>
						<th>First Approver Name</th>
						<th>Second Approver Name</th>
						<th>Approved By</th>
						<th>Approver Status</th>
						<th>Approved Date</th>
						<th>Approver Remark</th>
						<th>AppSupport Status</th>
						<th>Call Closed Date</th>
						<th>Remark</th>
						<th>Category</th>
						<th>Other Remark</th>
					</tr>
					<xsl:for-each select="NewDataSet/Table">
						<tr>
							<td><xsl:value-of select="TicketNumberPK"/></td>
							<td><xsl:value-of select="CallCreatedBy"/></td>
							<td><xsl:value-of select="AgentCode"/></td>
							<td><xsl:value-of select="SMBranchName"/></td>
							<td><xsl:value-of select="SMRegionName"/></td>
							<td><xsl:value-of select="SMZoneName"/></td>
							<td><xsl:value-of select="SMName"/></td>
							<td><xsl:value-of select="SMCode"/></td>
							<td><xsl:value-of select="ApplicationName"/></td>
							<td><xsl:value-of select="IssueRequestType"/></td>
							
							<td><xsl:value-of select="IssueRequestSubType"/></td>
							<td><xsl:value-of select="CallDate"/></td>
							
							<td><xsl:value-of select="UserRemark"/></td>
							<td><xsl:value-of select="ApproverName"/></td>
							<td><xsl:value-of select="SApproverName"/></td>
							<td><xsl:value-of select="ApprovedBy"/></td>

							<td><xsl:value-of select="ApproverStatus"/></td>
							<td><xsl:value-of select="ApproverClosedDate"/></td>
							
							<td><xsl:value-of select="ApproverRemark"/></td>
							<td><xsl:value-of select="AppSupportStatus"/></td>
							
							<td><xsl:value-of select="AppSupportCloseDate"/></td>
							<td><xsl:value-of select="AppSupportRemark"/></td>
							
							<td><xsl:value-of select="Category"/></td>
							<td><xsl:value-of select="OtherRemark"/></td>
						</tr>
					</xsl:for-each>
				</table>
			</body>
		</html>
	</xsl:template>

</xsl:stylesheet>


