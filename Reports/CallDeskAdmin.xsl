<?xml version="1.0" encoding="utf-8"?>

<xsl:stylesheet version="1.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

	<xsl:template match="/">
		<html>
			<body>
					<table border="2">
					<tr style="background-color:#CC0000;font-size:small;font-family:Arial" >
						<td colspan="41">
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

						<td colspan="38" >
						</td>
					</tr>
					<tr style="background-color:#CC0000;font-size:small;font-family:Arial" >
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
						<td colspan="39" >
						</td>
					</tr>
					<tr>
					</tr>
				</table>
				<table border="2">
					<tr style="background-color:#4169E1;font-size:small;font-family:Arial;color:White">
						<th>Ticket Number</th>
						<th>Call Registered by</th>
						<th>Branch ID</th>
						<th>Branch Name</th>
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
						<th>Performer</th>
						<th>Ticket Processing Group</th>
						<th>Call Closed Date</th>
						<th>Remark</th>
						<th>Category</th>
						<th>Other Remark</th>
						<th>Portal ID Locked</th>
						<th>Policy No For RN</th>
						<th>Ticket Value</th>
						<th>Phone Remark</th>
            <!-- [CR-12] RCA Start-->
            <th>RCA Type</th>
            <th>RCA Details</th>
            <th>RCA CreatedBy</th>
            <th>RCA CreatedDate</th>
            <!-- [CR-12] RCA End-->
			      <th>Insured Name</th>
            <th>Imd /Broker/Agent Name</th>
            <!--<th>UploadedDocument</th>-->
            <th>Occupancy/address</th>
            <th>Sum Insured/SM name</th>
            <th>Net Premium/Industry</th>
            <th>Previous Insurer Name</th>
            <th>TPA Name</th>
            <th>Policy Renewal Date</th>
            <th>Number Option Including Expiry</th>
            <th>Industry Type</th>
            <th>Policy/Claim/Proposal/CoverNote Number</th>
						
					</tr>
					<xsl:for-each select="NewDataSet/Table">
						<tr>
							<td><xsl:value-of select="TicketNumberPK"/></td>
							<td><xsl:value-of select="CallCreatedBy"/></td>							
							<td><xsl:value-of select="CallLoggedBranch"/></td>
							<td><xsl:value-of select="BranchName"/></td>
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
							 <td><xsl:value-of select="AppSuportPerformerName"/></td>
							 <td><xsl:value-of select="TicketProcessingGroup"/></td>
							<td><xsl:value-of select="AppSupportCloseDate"/></td>
							<td><xsl:value-of select="AppSupportRemark"/></td>							
							<td><xsl:value-of select="Category"/></td>
							<td><xsl:value-of select="OtherRemark"/></td>
							<td><xsl:value-of select="PortalIDLocked"/></td>
							<td><xsl:value-of select="PolicyNoRN"/></td>
							<td><xsl:value-of select="TicketValue"/></td>
							<td><xsl:value-of select="AppSupportPhoneRemark"/></td>
              
              <!-- [CR-12] RCA Start-->
              <td><xsl:value-of select="RCA_Type"/></td>
              <td><xsl:value-of select="RCA_Details"/></td>
              <td><xsl:value-of select="RCA_CreatedBy"/></td>
              <td><xsl:value-of select="RCA_CreatedDate"/></td>
              <!-- [CR-12] RCA End-->
			        <td><xsl:value-of select="InsuredName"/></td>
              <td><xsl:value-of select="IntermediaryName"/></td>
              <!-- <td><xsl:value-of select="UploadedScreen"/></td>-->
              <td><xsl:value-of select="Occupancy"/></td>
              <td><xsl:value-of select="SumInsured"/></td>
              <td><xsl:value-of select="NetPremium"/></td>              
              <td><xsl:value-of select="PreviousInsurerName"/></td>
              <td><xsl:value-of select="TPAName"/></td>
              <td><xsl:value-of select="PolicyRenewalDate"/></td>
              <td><xsl:value-of select="NumberOptionIncludingExpiry"/></td>
              <td><xsl:value-of select="IndustryType"/></td>
              <td><xsl:value-of select="ProposalNo"/></td>
							
						</tr>
					</xsl:for-each>
				</table>
			</body>
		</html>
	</xsl:template>

</xsl:stylesheet>


