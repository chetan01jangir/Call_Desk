	/*
	This Calender is suport only two format
	ie. dd-mm-yyyy
	    dd/mm/yyyy
	  
	  popUpCalendar(
	                    this = Leave this parameter as it is.
	                    ,txtDOBDate=Name of textbox where calender will set date after selection
	                    , vDateFormat=Date format 
	                    ,+5= Leave this parameter as it is.
	                    ,+10 Leave this parameter as it is.
	                    ,true=Allow back date selection
	                    ,false=allow future date selection
	                    ,0=No of back date you want to allow. if this parameter is >0 then 6 parameter must be false.
	                    ,0=No of future date you want to allow. if this parameter is >0 then 7 parameter must be false.
	                    ,0=Configuration Index.if you dont have any configuration then pass this parameter -1,Define your configuration in Line no 895.
	                    )
	                    
	     For freetext input and validation add following code in your textbox
	     onkeypress="CatchAlphabetOrSpecialCharacter(this)" onblur="CheckData(this)"
	     
	     Same code to use this code
	     --------------------------------------------------------------------
	     <html>
	     <head runat="server">
            <title>Calender Page</title>
            <% Response.Write(" <script> var	today =	new	Date('" + DateTime.Now.Date + "') </script>");%>   
            <script type="text/javascript" language="javascript" src="1popcalendar.js"></script>
            <script type="text/javascript" language="JavaScript">vDateFormat='dd/mm/yyyy'</script>
           </head>
           <body>
           <table>
            <tr>
              <td >Risk Start Date</td>
                <td style="width: 232px" >
                   <input id="Text2"   runat="server" maxlength="15" onkeypress="CatchAlphabetOrSpecialCharacter(this)"
                    style="border-right: 1px solid; border-top: 1px solid; font-size: 12px; border-left: 1px solid;
                    color: black; border-bottom: 1px solid; width: 123px;" type="text" onblur="CheckData(this)" />
                   <script type="text/javascript">                                   
                    if (!document.layers)
                    { 
                        document.write("<img id='calendar1' align=absmiddle style='CURSOR:hand' src='./Images/Calender1.gif' onclick='popUpCalendar(this,Text2, vDateFormat,+5,+10,false,false,-3,3,2)' alt='select'>")
                    }   
                   </script>
                  </td>                    
              </tr>
           </table>
           </body>
           </html>
           
	*/
	
	
	if(typeof vLangue == 'undefined')
		vLangue = 1
	if(typeof vWeekManagement == 'undefined')
		vWeekManagement = 1
		
	
	
	var	fixedX = -1					// x position (-1 if to appear below control)
	var	fixedY = -1					// y position (-1 if to appear below control)
	var startAt = parseFloat(vWeekManagement)   // 0 - sunday ; 1 - monday
	var showWeekNumber = 1			// 0 - don't show; 1 - show
	var showToday = 1				// 0 - don't show; 1 - show
	var imgDir = "../images/"					// directory for images ... e.g. var imgDir="/images/"
	var gotoString = "Go To Current Month"
	var todayString = "Today is"
	var weekString = "Wk"
	var scrollLeftMessage = "Click to scroll to previous month. Hold mouse button to scroll automatically."
	var scrollRightMessage = "Click to scroll to next month. Hold mouse button to scroll automatically."
	var selectMonthMessage = "Click to select a month."
	var selectYearMessage = "Click to select a year."
	var selectDateMessage = "Select [date] as date." // do not replace [date], it will be replaced by date.
	var altCloseCalendar = "Close the Calendar"
	var	monthName =	new	Array("January","February","March","April","May","June","July","August","September","October","November","December")
	
	dayName = new Array	("Sun","Mon","Tue","Wed","Thu","Fri","Sat")
	arrTemp = dayName.slice(startAt,7)
	dayName = arrTemp.concat(dayName.slice(0,startAt))
	
	var _SelectBackDated = true;
	var _SelectFutureDate = true;
	var _BackDays = 0;
	var _FutureDays = 0;
	var _serverDate = null;
	var _DateSeperator = "-";
   
	
	if (vLangue==0) //FRENCH
		{
		gotoString = "Aller au mois en cours"
		todayString = "Aujourd'hui :&nbsp;"
		weekString = "Sem"
		scrollLeftMessage = "Cliquer pour le mois précédent. Tenir enfoncé pour déroulement automatique."
		scrollRightMessage = "Cliquer pour le mois suivant. Tenir enfoncé pour déroulement automatique."
		selectMonthMessage = "Cliquer pour choisir un mois."
		selectYearMessage = "Clicquer pour choisir une année."
		selectDateMessage = "Choisir [date] comme date." // do not replace [date], it will be replaced by date.
		altCloseCalendar = "Fermer le calendrier"
		monthName =	new	Array("Janvier","Février","Mars","Avril","Mai","Juin","Juillet","Août","Septembre","Octobre","Novembre","Décembre")

		dayName = new Array	("Dim","Lun","Mar","Mer","Jeu","Ven","Sam")
		fullDayName = new Array	("dimanche","lundi","mardi","mercredi","jeudi","vendredi","samedi")
		
		arrTemp = dayName.slice(startAt,7)
		dayName = arrTemp.concat(dayName.slice(0,startAt))
		
		arrTemp = fullDayName.slice(startAt,7)
		fullDayName = arrTemp.concat(fullDayName.slice(0,startAt))
		}
	
	var	crossobj, crossMonthObj, crossYearObj, monthSelected, yearSelected, dateSelected, omonthSelected, oyearSelected, odateSelected, monthConstructed, yearConstructed, intervalID1, intervalID2, timeoutID1, timeoutID2, ctlToPlaceValue, ctlNow, dateFormat, nStartingYear

	var	bPageLoaded=false
	var	ie=document.all
	var	dom=document.getElementById

	var	ns4=document.layers
	//var	today =	new	Date()//Todays Date
	var	dateNow	 = today.getDate()
	var	monthNow = today.getMonth()
	var	yearNow	 = today.getYear()
	var dtCDate = new	Date(today);
	var	imgsrc = new Array("Cdrop1.gif","Cdrop2.gif","Cleft1.gif","Cleft2.gif","Cright1.gif","Cright2.gif")
	var	img	= new Array()

	var bShow = false;


//Check datetime
function CheckData(varValue)
{
   if(varValue.value !="")
   {
    if(isDate(varValue.value)==true)
    {
        var StartDate ;
        var EndDate ;
        var CDate1 = new Date(today);
        var CDate2 = new Date(today);
        
        //var UserDate = new Date();
        
      	if(_SelectBackDated == true)
      	{
      	    StartDate = new Date("01" + _DateSeperator +"01" + _DateSeperator +"1753");
      	}
      	else
      	{
      	    StartDate = new Date(CDate1.dateAdd("d",_BackDays));
      	}
      	
      	if(_SelectFutureDate == true)
      	{
      	    EndDate = new Date("31-12-9999");
      	}
      	else
      	{
      	    EndDate = new Date(CDate2.dateAdd("d",_FutureDays));
      	}

        var uDate1,sDate1;
       //debugger;
       
        if(dateFormat==null) 
        {
            dateFormat ='dd/MM/yyyy'
        }
       
       if( dateFormat.charAt(0).toUpperCase()=='D')
       {
        uDate1 = varValue.value.toString().split(_DateSeperator)[1] + "/" + varValue.value.toString().split(_DateSeperator)[0] + "/" + varValue.value.toString().split(_DateSeperator)[2];
       }
        else
        {
        uDate1 = varValue.value.toString().split(_DateSeperator)[0] + "/" + varValue.value.toString().split(_DateSeperator)[1] + "/" + varValue.value.toString().split(_DateSeperator)[2];
        }
        sDate1 = StartDate.getMonth()+1 + "/" + StartDate.getDate() + "/" + StartDate.getYear();
        eDate1 = EndDate.getMonth()+1 + "/" + EndDate.getDate() + "/" + EndDate.getYear();

      	if((DateDiff(uDate1,sDate1)<0) || ((DateDiff(uDate1,eDate1))>0))
      	{
      	     if( dateFormat.charAt(0).toUpperCase()=='D')
            {
      	        alert("Date must be between " + (StartDate.getDate()<9?"0" +StartDate.getDate() : StartDate.getDate() ) + _DateSeperator + ((StartDate.getMonth()+1)<9?"0"+(StartDate.getMonth()+1):(StartDate.getMonth()+1)) + _DateSeperator + StartDate.getYear()  + " (dd" +_DateSeperator + "mm" + _DateSeperator + "yyyy) and " + (EndDate.getDate()<9?"0" +EndDate.getDate() : EndDate.getDate() ) + _DateSeperator + ((EndDate.getMonth()+1)<9?"0"+(EndDate.getMonth()+1):(EndDate.getMonth()+1)) + _DateSeperator + EndDate.getYear() + " (dd" +_DateSeperator + "mm" +_DateSeperator + "yyyy).");
      	    }
      	    else
      	    {
      	        alert("Date must be between " + ((StartDate.getMonth()+1)<9?"0"+(StartDate.getMonth()+1):(StartDate.getMonth()+1)) + _DateSeperator +  (StartDate.getDate()<9?"0" +StartDate.getDate() : StartDate.getDate() ) + _DateSeperator + StartDate.getYear()  + " (mm" +_DateSeperator + "dd" + _DateSeperator + "yyyy) and " + ((EndDate.getMonth()+1)<9?"0"+(EndDate.getMonth()+1):(EndDate.getMonth()+1)) + _DateSeperator +  (EndDate.getDate()<9?"0" +EndDate.getDate() : EndDate.getDate() ) + _DateSeperator + EndDate.getYear() + " (mm" +_DateSeperator + "dd" +_DateSeperator + "yyyy).");
      	    }
      	     varValue.value = "";
             varValue.focus();
      	    
      	}

    }
    else
    {
        varValue.value = "";
        varValue.focus();
    }
   }
}

function dateAddExtention(p_Interval, p_Number){ 


   var thing = new String(); 
    
    
   //in the spirt of VB we'll make this function non-case sensitive 
   //and convert the charcters for the coder. 
   p_Interval = p_Interval.toLowerCase(); 
    
   if(isNaN(p_Number)){ 
    
      //Only accpets numbers  
      //throws an error so that the coder can see why he effed up    
      throw "The second parameter must be a number. \n You passed: " + p_Number; 
      return false; 
   } 

   p_Number = new Number(p_Number); 
   switch(p_Interval.toLowerCase()){ 
      case "yyyy": {// year 
         this.setFullYear(this.getFullYear() + p_Number); 
         break; 
      } 
      case "q": {      // quarter 
         this.setMonth(this.getMonth() + (p_Number*3)); 
         break; 
      } 
      case "m": {      // month 
         this.setMonth(this.getMonth() + p_Number); 
         break; 
      } 
      case "y":      // day of year 
      case "d":      // day 
      case "w": { 
                 // weekday 
         this.setDate(this.getDate() + p_Number); 
         break; 
      } 
      case "ww": {   // week of year 
         this.setDate(this.getDate() + (p_Number*7)); 
         break; 
      } 
      case "h": {      // hour 
         this.setHours(this.getHours() + p_Number); 
         break; 
      } 
      case "n": {      // minute 
         this.setMinutes(this.getMinutes() + p_Number); 
         break; 
      } 
      case "s": {      // second 
         this.setSeconds(this.getSeconds() + p_Number); 
         break; 
      } 
      case "ms": {      // second 
         this.setMilliseconds(this.getMilliseconds() + p_Number); 
         break; 
      } 
      default: { 
       
         //throws an error so that the coder can see why he effed up and 
         //a list of elegible letters. 
         throw   "The first parameter must be a string from this list: \n" + 
               "yyyy, q, m, y, d, w, ww, h, n, s, or ms.  You passed: " + p_Interval; 
         return false; 
      } 
   } 
   return this; 
} 
Date.prototype.dateAdd = dateAddExtention; 

function isDate(dateStr) {
var datePat = /^(\d{1,2})(\/|-)(\d{1,2})(\/|-)(\d{4})$/;
var matchArray = dateStr.match(datePat); // is the format ok?

if (matchArray == null) {
alert("Please enter date as dd/mm/yyyy. Your current selection reads: " + dateStr);
return false;
}

day = matchArray[1]; // p@rse date into variables
month = matchArray[3];
year = matchArray[5];

if (month < 1 || month > 12) { // check month range
alert("Month must be between 1 and 12.");
return false;
}

if (day < 1 || day > 31) {
alert("Day must be between 1 and 31.");
return false;
}

if (year < 1753) {
alert("Year must be after 1753.");
return false;
}


if ((month==4 || month==6 || month==9 || month==11) && day==31) {
alert("Month "+month+" doesn`t have 31 days!");
return false;
}

if (month == 2) { // check for february 29th
var isleap = (year % 4 == 0 && (year % 100 != 0 || year % 400 == 0));
if (day > 29 || (day==29 && !isleap)) {
alert("February " + year + " doesn`t have " + day + " days!");
return false;
}
}
return true; // date is valid
}




	/* hides <select> and <applet> objects (for IE only) */
	function hideElement( elmID, overDiv )
	{
	  if( ie )
	  {
		for( i = 0; i < document.all.tags( elmID ).length; i++ )
		{
		  obj = document.all.tags( elmID )[i];
		  if( !obj || !obj.offsetParent )
		  {
			continue;
		  }
	  
		  // Find the element's offsetTop and offsetLeft relative to the BODY tag.
		  objLeft   = obj.offsetLeft;
		  objTop    = obj.offsetTop;
		  objParent = obj.offsetParent;
		  
		  while( objParent.tagName.toUpperCase() != "BODY" )
		  {
			objLeft  += objParent.offsetLeft;
			objTop   += objParent.offsetTop;
			objParent = objParent.offsetParent;
		  }
	  
		  objHeight = obj.offsetHeight;
		  objWidth = obj.offsetWidth;
	  
		  if(( overDiv.offsetLeft + overDiv.offsetWidth ) <= objLeft );
		  else if(( overDiv.offsetTop + overDiv.offsetHeight ) <= objTop );
		  else if( overDiv.offsetTop >= ( objTop + objHeight ));
		  else if( overDiv.offsetLeft >= ( objLeft + objWidth ));
		  else
		  {
			obj.style.visibility = "hidden";
		  }
		}
	  }
	}
	 
	/*
	* unhides <select> and <applet> objects (for IE only)
	*/
	function showElement( elmID )
	{
	  if( ie )
	  {
		for( i = 0; i < document.all.tags( elmID ).length; i++ )
		{
		  obj = document.all.tags( elmID )[i];
		  
		  if( !obj || !obj.offsetParent )
		  {
			continue;
		  }
		
		  obj.style.visibility = "";
		}
	  }
	}
	
	 function DateDiff(StartDate,EndDate)
	{
	  // var strDate1 = document.formName.fieldName.value 
	 //   var strDate2 = document.formName.fieldName.value 
		datDate1= Date.parse(StartDate); 
		datDate2= Date.parse(EndDate); 
		return ((datDate1-datDate2)/(24*60*60*1000)) 
	}

	function HolidayRec (d, m, y, desc)
	{
		this.d = d
		this.m = m
		this.y = y
		this.desc = desc
	}

	var HolidaysCounter = 0
	var Holidays = new Array()

	function addHoliday (d, m, y, desc)
	{
		Holidays[HolidaysCounter++] = new HolidayRec ( d, m, y, desc )
	}

	if (dom)
	{
		for	(i=0;i<imgsrc.length;i++)
		{
			img[i] = new Image
			img[i].src = imgDir + imgsrc[i]
		}
		document.write ("<div onclick='bShow=true' id='calendar'	style='z-index:+999;position:absolute;visibility:hidden;'><table	width="+((showWeekNumber==1)?250:220)+" style='font-family:arial;font-size:11px;border-width:1;border-style:solid;border-color:#a0a0a0;font-family:arial; font-size:11px}' bgcolor='#ffffff'><tr bgcolor='#0000aa'><td><table width='"+((showWeekNumber==1)?248:218)+"'><tr><td style='padding:2px;font-family:arial; font-size:11px;'><font color='#ffffff'><B><span id='caption'></span></B></font></td><td align=right><a href='javascript:hideCalendar()'><IMG SRC='"+imgDir+"close.gif' name=close WIDTH='15' HEIGHT='13' BORDER='0' ALT='" + altCloseCalendar + "'></a></td></tr></table></td></tr><tr><td style='padding:5px' bgcolor=#ffffff><span id='content'></span></td></tr>")
			
		if (showToday==1)
		{
			document.write ("<tr bgcolor=#f0f0f0><td style='padding:5px' align=center><span id='lblToday'></span></td></tr>")
		}
			
		document.write ("</table></div><div id='selectMonth' style='z-index:+999;position:absolute;visibility:hidden;'></div><div id='selectYear' style='z-index:+999;position:absolute;visibility:hidden;'></div>");
	}

	var	styleAnchor="text-decoration:none;color:black;"
	var	styleLightBorder="border-style:solid;border-width:1px;border-color:#a0a0a0;"

	function swapImage(srcImg, destImg){
		if (ie)	{ document.getElementById(srcImg).setAttribute("src",imgDir + destImg) }
	}

	function init()	{
		if (!ns4)
		{
			if (!ie) { yearNow += 1900	}

			crossobj=(dom)?document.getElementById("calendar").style : ie? document.all.calendar : document.calendar
			hideCalendar()

			crossMonthObj=(dom)?document.getElementById("selectMonth").style : ie? document.all.selectMonth	: document.selectMonth

			crossYearObj=(dom)?document.getElementById("selectYear").style : ie? document.all.selectYear : document.selectYear

			monthConstructed=false;
			yearConstructed=false;
			//showToday=0;
			if (showToday==1)
			{
				if (vLangue)
					document.getElementById("lblToday").innerHTML =	todayString + " <a onmousemove='window.status=\""+gotoString+"\"' onmouseout='window.status=\"\"' title='"+gotoString+"' style='"+styleAnchor+"' href='javascript:monthSelected=monthNow;yearSelected=yearNow;constructCalendar();'>"+dayName[firstdayofweek(today.getDay())]+", " + dateNow + " " + monthName[monthNow].substring(0,3)	+ "	" +	yearNow	+ "</a>"
				else
					document.getElementById("lblToday").innerHTML =	todayString + " <a onmousemove='window.status=\""+gotoString+"\"' onmouseout='window.status=\"\"' title='"+gotoString+"' style='"+styleAnchor+"' href='javascript:monthSelected=monthNow;yearSelected=yearNow;constructCalendar();'>"+fullDayName[firstdayofweek(today.getDay())]+" le " + ((dateNow==1)?"1<sup>er</sup>":dateNow) + " " + monthName[monthNow].toLowerCase()	+ "	" +	yearNow	+ "</a>"
			}

			sHTML1="<span id='spanLeft'	style='border-style:solid;border-width:1;border-color:#3366FF;cursor:pointer' onmouseover='swapImage(\"changeLeft\",\"left2.gif\");this.style.borderColor=\"#88AAFF\";window.status=\""+scrollLeftMessage+"\"' onclick='javascript:decMonth()' onmouseout='clearInterval(intervalID1);swapImage(\"changeLeft\",\"left2.gif\");this.style.borderColor=\"#3366FF\";window.status=\"\"' onmousedown='clearTimeout(timeoutID1);timeoutID1=setTimeout(\"StartDecMonth()\",500)'	onmouseup='clearTimeout(timeoutID1);clearInterval(intervalID1)'>&nbsp<IMG id='changeLeft' SRC='"+imgDir+"left2.gif' width=10 height=11 BORDER=0>&nbsp</span>&nbsp;"
			sHTML1+="<span id='spanRight' style='border-style:solid;border-width:1;border-color:#3366FF;cursor:pointer'	onmouseover='swapImage(\"changeRight\",\"right2.gif\");this.style.borderColor=\"#88AAFF\";window.status=\""+scrollRightMessage+"\"' onmouseout='clearInterval(intervalID1);swapImage(\"changeRight\",\"right2.gif\");this.style.borderColor=\"#3366FF\";window.status=\"\"' onclick='incMonth()' onmousedown='clearTimeout(timeoutID1);timeoutID1=setTimeout(\"StartIncMonth()\",500)'	onmouseup='clearTimeout(timeoutID1);clearInterval(intervalID1)'>&nbsp<IMG id='changeRight' SRC='"+imgDir+"right2.gif'	width=10 height=11 BORDER=0>&nbsp</span>&nbsp"
			sHTML1+="<span id='spanMonth' style='border-style:solid;border-width:1;border-color:#3366FF;cursor:pointer'	onmouseover='swapImage(\"changeMonth\",\"drop2.gif\");this.style.borderColor=\"#88AAFF\";window.status=\""+selectMonthMessage+"\"' onmouseout='swapImage(\"changeMonth\",\"drop2.gif\");this.style.borderColor=\"#3366FF\";window.status=\"\"' onclick='popUpMonth()'></span>&nbsp;"
			sHTML1+="<span id='spanYear' style='border-style:solid;border-width:1;border-color:#3366FF;cursor:pointer' onmouseover='swapImage(\"changeYear\",\"drop2.gif\");this.style.borderColor=\"#88AAFF\";window.status=\""+selectYearMessage+"\"'	onmouseout='swapImage(\"changeYear\",\"drop2.gif\");this.style.borderColor=\"#3366FF\";window.status=\"\"'	onclick='popUpYear()'></span>&nbsp;"
			
			document.getElementById("caption").innerHTML  =	sHTML1

			bPageLoaded=true
		}
	}
	function firstdayofweek(day)
	{
	day -= startAt
	if (day < 0){day = 7 + day}
	return day
	}

	function hideCalendar()	{
		crossobj.visibility="hidden"
		if (crossMonthObj != null){crossMonthObj.visibility="hidden"}
		if (crossYearObj !=	null){crossYearObj.visibility="hidden"}

		showElement( 'SELECT' );
		showElement( 'APPLET' );
	}

	function padZero(num) {
		return (num	< 10)? '0' + num : num ;
	}

	function constructDate(d,m,y)
	{
		sTmp = dateFormat
		sTmp = sTmp.replace	("dd","<e>")
		sTmp = sTmp.replace	("d","<d>")
		sTmp = sTmp.replace	("<e>",padZero(d))
		sTmp = sTmp.replace	("<d>",d)
		sTmp = sTmp.replace	("mmm","<o>")
		sTmp = sTmp.replace	("mm","<n>")
		sTmp = sTmp.replace	("m","<m>")
		sTmp = sTmp.replace	("<m>",m+1)
		sTmp = sTmp.replace	("<n>",padZero(m+1))
		sTmp = sTmp.replace	("<o>",monthName[m])
		return sTmp.replace ("yyyy",y)
	}

	function closeCalendar() {
	
		var	sTmp
		

		hideCalendar();
		ctlToPlaceValue.value =	constructDate(dateSelected,monthSelected,yearSelected)
		ctlToPlaceValue.focus();
	}

	/*** Month Pulldown	***/

	function StartDecMonth()
	{
		intervalID1=setInterval("decMonth()",80)
	}

	function StartIncMonth()
	{
		intervalID1=setInterval("incMonth()",80)
	}

	function incMonth () {
		monthSelected++
		if (monthSelected>11) {
			monthSelected=0
			yearSelected++
		}
		constructCalendar()
	}

	function decMonth () {
		monthSelected--
		if (monthSelected<0) {
			monthSelected=11
			yearSelected--
		}
		constructCalendar()
	}

	function constructMonth() {
		popDownYear()
		if (!monthConstructed) {
			sHTML =	""
			for	(i=0; i<12;	i++) {
				sName =	monthName[i];
				if (i==monthSelected){
					sName =	"<B>" +	sName +	"</B>"
				}
				sHTML += "<tr><td id='m" + i + "' onmouseover='this.style.backgroundColor=\"#FFCC99\"' onmouseout='this.style.backgroundColor=\"\"' style='cursor:pointer' onclick='monthConstructed=false;monthSelected=" + i + ";constructCalendar();popDownMonth();event.cancelBubble=true'>&nbsp;" + sName + "&nbsp;</td></tr>"
			}

			document.getElementById("selectMonth").innerHTML = "<table width=70	style='font-family:arial; font-size:11px; border-width:1; border-style:solid; border-color:#a0a0a0;' bgcolor='#FFFFDD' cellspacing=0 onmouseover='clearTimeout(timeoutID1)'	onmouseout='clearTimeout(timeoutID1);timeoutID1=setTimeout(\"popDownMonth()\",100);event.cancelBubble=true'>" +	sHTML +	"</table>"

			monthConstructed=true
		}
	}

	function popUpMonth() {
		constructMonth()
		crossMonthObj.visibility = (dom||ie)? "visible"	: "show"
		crossMonthObj.left = parseInt(crossobj.left) + 50
		crossMonthObj.top =	parseInt(crossobj.top) + 26

		hideElement( 'SELECT', document.getElementById("selectMonth") );
		hideElement( 'APPLET', document.getElementById("selectMonth") );			
	}

	function popDownMonth()	{
		crossMonthObj.visibility= "hidden"
	}

	/*** Year Pulldown ***/

	function incYear() {
	
		for	(i=0; i<7; i++){
			newYear	= (i+nStartingYear)+1
			if (newYear==yearSelected)
			{ txtYear =	"&nbsp;<B>"	+ newYear +	"</B>&nbsp;" }
			else
			{ txtYear =	"&nbsp;" + newYear + "&nbsp;" }
			document.getElementById("y"+i).innerHTML = txtYear

		}
		nStartingYear ++;
		bShow=true
	}

	function decYear() {
		for	(i=0; i<7; i++){
			newYear	= (i+nStartingYear)-1
			if (newYear==yearSelected)
			{ txtYear =	"&nbsp;<B>"	+ newYear +	"</B>&nbsp;" }
			else
			{ txtYear =	"&nbsp;" + newYear + "&nbsp;" }
			document.getElementById("y"+i).innerHTML = txtYear
		}
		nStartingYear --;
		bShow=true
	}

	function selectYear(nYear) {
		yearSelected=parseInt(nYear+nStartingYear);
		yearConstructed=false;
		constructCalendar();
		popDownYear();
	}

	function constructYear() {
		popDownMonth()
		sHTML =	""
		if (!yearConstructed) {

			sHTML =	"<tr><td align='center'	onmouseover='this.style.backgroundColor=\"#FFCC99\"' onmouseout='clearInterval(intervalID1);this.style.backgroundColor=\"\"' style='cursor:pointer'	onmousedown='clearInterval(intervalID1);intervalID1=setInterval(\"decYear()\",30)' onmouseup='clearInterval(intervalID1)'>-</td></tr>"

			j =	0
			nStartingYear =	yearSelected-3
			for	(i=(yearSelected-3); i<=(yearSelected+3); i++) {
				sName =	i;
				if (i==yearSelected){
					sName =	"<B>" +	sName +	"</B>"
				}

				sHTML += "<tr><td id='y" + j + "' onmouseover='this.style.backgroundColor=\"#FFCC99\"' onmouseout='this.style.backgroundColor=\"\"' style='cursor:pointer' onclick='selectYear("+j+");event.cancelBubble=true'>&nbsp;" + sName + "&nbsp;</td></tr>"
				j ++;
			}

			sHTML += "<tr><td align='center' onmouseover='this.style.backgroundColor=\"#FFCC99\"' onmouseout='clearInterval(intervalID2);this.style.backgroundColor=\"\"' style='cursor:pointer' onmousedown='clearInterval(intervalID2);intervalID2=setInterval(\"incYear()\",30)'	onmouseup='clearInterval(intervalID2)'>+</td></tr>"

			document.getElementById("selectYear").innerHTML	= "<table width=44 style='font-family:arial; font-size:11px; border-width:1; border-style:solid; border-color:#a0a0a0;'	bgcolor='#FFFFDD' onmouseover='clearTimeout(timeoutID2)' onmouseout='clearTimeout(timeoutID2);timeoutID2=setTimeout(\"popDownYear()\",100)' cellspacing=0>"	+ sHTML	+ "</table>"

			yearConstructed	= true
		}
	}

	function popDownYear() {
		clearInterval(intervalID1)
		clearTimeout(timeoutID1)
		clearInterval(intervalID2)
		clearTimeout(timeoutID2)
		crossYearObj.visibility= "hidden"
	}

	function popUpYear() {
		var	leftOffset

		constructYear()
		crossYearObj.visibility	= (dom||ie)? "visible" : "show"
		leftOffset = parseInt(crossobj.left) + document.getElementById("spanYear").offsetLeft
		if (ie)
		{
			leftOffset += 6
		}
		crossYearObj.left =	leftOffset
		crossYearObj.top = parseInt(crossobj.top) +	26
	}

	/*** calendar ***/
   function WeekNbr(n) {
	  // Algorithm used:
	  // From Klaus Tondering's Calendar document (The Authority/Guru)
	  // hhtp://www.tondering.dk/claus/calendar.html
	  // a = (14-month) / 12
	  // y = year + 4800 - a
	  // m = month + 12a - 3
	  // J = day + (153m + 2) / 5 + 365y + y / 4 - y / 100 + y / 400 - 32045
	  // d4 = (J + 31741 - (J mod 7)) mod 146097 mod 36524 mod 1461
	  // L = d4 / 1460
	  // d1 = ((d4 - L) mod 365) + L
	  // WeekNumber = d1 / 7 + 1
 
	  year = n.getFullYear();
	  month = n.getMonth() + 1;
	  /*
	  if (startAt == 0) {
		 day = n.getDate() + 1;
	  }
	  else {
		 day = n.getDate();
	  }*/
	  day = n.getDate() + 1-startAt;
 
	  a = Math.floor((14-month) / 12);
	  y = year + 4800 - a;
	  m = month + 12 * a - 3;
	  b = Math.floor(y/4) - Math.floor(y/100) + Math.floor(y/400);
	  J = day + Math.floor((153 * m + 2) / 5) + 365 * y + b - 32045;
	  d4 = (((J + 31741 - (J % 7)) % 146097) % 36524) % 1461;
	  L = Math.floor(d4 / 1460);
	  d1 = ((d4 - L) % 365) + L;
	  week = Math.floor(d1/7) + 1;
 
	  return week;
   }

	function constructCalendar () {
	
		var aNumDays = Array (31,0,31,30,31,30,31,31,30,31,30,31)

		var dateMessage
		var	startDate =	new	Date (yearSelected,monthSelected,1)
		var endDate

		if (monthSelected==1)
		{
			endDate	= new Date (yearSelected,monthSelected+1,1);
			endDate	= new Date (endDate	- (24*60*60*1000));
			numDaysInMonth = endDate.getDate()
		}
		else
		{
			numDaysInMonth = aNumDays[monthSelected];
		}

		datePointer	= 0
		//dayPointer = startDate.getDay()
		dayPointer = firstdayofweek(startDate.getDay())
		/*
		switch (startAt)
			{
			case (0): dayPointer = dayPointer
			break;
			case (1): dayPointer--
			break;
			case (6): dayPointer++
			break;
			}	
			*/
		//dayPointer = startDate.getDay()// - startAt
		
		if (dayPointer<0)
		{
			//dayPointer = 6
		}

		sHTML =	"<table	 border=0 style='font-family:verdana;font-size:10px;'><tr>"

		if (showWeekNumber==1)
		{
			sHTML += "<td width=27><b>" + weekString + "</b></td><td width=1 rowspan=7 bgcolor='#d0d0d0' style='padding:0px'><img src='"+imgDir+"divider.gif' width=1></td>"
		}

		for	(i=0; i<7; i++)	{
			sHTML += "<td width='27' align='right'><B>"+ dayName[i]+"</B></td>"
		}
		sHTML +="</tr><tr>"
		
		if (showWeekNumber==1)
		{
			sHTML += "<td align=right>" + WeekNbr(startDate) + "&nbsp;</td>"
		}

		for	( var i=1; i<=dayPointer;i++ )
		{
			sHTML += "<td>&nbsp;</td>"
		}
	
		for	( datePointer=1; datePointer<=numDaysInMonth; datePointer++ )
		{
			dayPointer++;
			sHTML += "<td align=right>"
			sStyle=styleAnchor
			if ((datePointer==odateSelected) &&	(monthSelected==omonthSelected)	&& (yearSelected==oyearSelected))
			{ sStyle+=styleLightBorder }

			sHint = ""
			for (k=0;k<HolidaysCounter;k++)
			{
				if ((parseInt(Holidays[k].d)==datePointer)&&(parseInt(Holidays[k].m)==(monthSelected+1)))
				{
					if ((parseInt(Holidays[k].y)==0)||((parseInt(Holidays[k].y)==yearSelected)&&(parseInt(Holidays[k].y)!=0)))
					{
						sStyle+="background-color:#FFDDDD;"
						sHint+=sHint==""?Holidays[k].desc:"\n"+Holidays[k].desc
					}
				}
			}

			var regexp= /\"/g
			sHint=sHint.replace(regexp,"&quot;")

			dateMessage = "onmousemove='window.status=\""+selectDateMessage.replace("[date]",constructDate(datePointer,monthSelected,yearSelected))+"\"' onmouseout='window.status=\"\"' "

			//var dtCDate = new Date()
			//alert(dtCDate);
			var DtStart = monthSelected+1 + '/' + datePointer + '/' + yearSelected
			var DtCurrent = dtCDate.getMonth()+1 + '/' + dtCDate.getDate() + '/' + dtCDate.getYear()
			var BoolStrike = false;

			
			var DateText = datePointer;
			
			 var BackDateText1 = "<a "+dateMessage+" title=\"" + sHint + "\" style='"+sStyle+"' href='javascript:dateSelected="+datePointer + ";closeCalendar();'>&nbsp;<font color=#909090>" + DateText + "</font>&nbsp;</a>" 
			 var BackDateText2 = "<a "+dateMessage+" title=\"" + sHint + "\" style='"+sStyle+"' href='javascript:dateSelected="+datePointer + ";closeCalendar();'>&nbsp;" + DateText + "&nbsp;</a>" 
			
			if(_SelectBackDated == false)
			{
				if(DateDiff(DtStart,DtCurrent)<_BackDays)
				{
					DateText = '<strike>' + datePointer + '</strike>'
					 var BackDateText1 = "&nbsp;<font color=#909090>" + DateText + "</font>&nbsp;" 
					 var BackDateText2 = "&nbsp;" + DateText + "&nbsp;"
					 BoolStrike = true; 
				}
			}
			
			if(_SelectFutureDate == false)
			{
				if(DateDiff(DtStart,DtCurrent)>_FutureDays)
				{
					DateText = '<strike>' + datePointer + '</strike>'
					 var BackDateText1 = "&nbsp;<font color=#909090>" + DateText + "</font>&nbsp;" 
					 var BackDateText2 = "&nbsp;" + DateText + "&nbsp;" 
					 BoolStrike = true; 
				}
			}
			
			if ((datePointer==dateNow)&&(monthSelected==monthNow)&&(yearSelected==yearNow))
			{
			    if(BoolStrike)
				    sHTML += "<b><font color=#ff0000>&nbsp;" + DateText + "</font>&nbsp;</b>"
			    else
				    sHTML += "<b><a "+dateMessage+" title=\"" + sHint + "\" style='"+sStyle+"' href='javascript:dateSelected="+datePointer+";closeCalendar();'><font color=#ff0000>&nbsp;" + DateText + "</font>&nbsp;</a></b>"
			}

			else if	(dayPointer % 7 == (startAt * -1)+1)
			{ 
				sHTML += BackDateText1
			}
			else
			{ 
			   sHTML += BackDateText2
			}

			sHTML += ""
			if ((dayPointer+startAt) % 7 == startAt) { 
				sHTML += "</tr><tr>" 
				if ((showWeekNumber==1)&&(datePointer<numDaysInMonth))
				{
					sHTML += "<td align=right>" + (WeekNbr(new Date(yearSelected,monthSelected,datePointer+1))) + "&nbsp;</td>"
				}
			}
		}

		document.getElementById("content").innerHTML   = sHTML
		document.getElementById("spanMonth").innerHTML = "&nbsp;" +	monthName[monthSelected] + "&nbsp;<IMG id='changeMonth' SRC='"+imgDir+"drop2.gif' WIDTH='12' HEIGHT='10' BORDER=0>"
		document.getElementById("spanYear").innerHTML =	"&nbsp;" + yearSelected	+ "&nbsp;<IMG id='changeYear' SRC='"+imgDir+"drop2.gif' WIDTH='12' HEIGHT='10' BORDER=0>"
	}
	
	//Keyboard Event trapper
	
	function CatchAlphabetOrSpecialCharacter(varTextBox)
	{
		if(varTextBox.value.length==10)
		{
		 event.returnValue = false;
		}
		
	 if (event.keyCode < 45 ||event.keyCode > 57)
		{
			if(event.keyCode != 43 & event.keyCode != 45 & event.keyCode != 40  &  event.keyCode != 41  &  event.keyCode != 43  &  event.keyCode != 95)
			 {
				event.returnValue = false;
			 }            
		}
	   // alert( event.keyCode)
	   if(varTextBox.value.length==1)
		{
		   if( varTextBox.value>3)
		   {
				varTextBox.value =  '0' + varTextBox.value ;
		   }
		}
		
		if(varTextBox.value.length==4)
		{
		   if( varTextBox.value.charAt(3)>1)
		   {
				varTextBox.value = varTextBox.value.substring(0,3)+'0'+  varTextBox.value.charAt(3) ;
		   }
		}
		
	   if(varTextBox.value.length==2)
	   {
			if(varTextBox.value>31)
			{
				event.returnValue = false;
				return;
			}
		 //varTextBox.value = varTextBox.value + '-';
	   }
	   
		 if(varTextBox.value.length==5)
		  {

			if(varTextBox.value.substring(4,2)>2)
			{
				event.returnValue = false;
				return;
			}
			//varTextBox.value = varTextBox.value + '-';
		  }
	}
	//
	
	function popUpCalendar(ctl,	ctl2, format, top, left,DisplayBackDate,DisplayFutureDate,BackDays,FutureDays,ConfigIndex) {
	//debugger;
	 //Calender Config start here
		var configs=new Array();
		configs[0]="true,false,0,0"; //DOB validation
		configs[1]="false,false,-150,0"; //Check Validation
		configs[2]="false,false,-3,3"; //Risk start date end date validation
	//Calender Config end here
	
	  if(ConfigIndex >=0)
	  {
		_SelectBackDated =  configs[ConfigIndex].toString().split(',')[0]=="true"?true:false;
		_SelectFutureDate = configs[ConfigIndex].toString().split(',')[1]=="true"?true:false;
		_BackDays=parseInt(configs[ConfigIndex].toString().split(',')[2])
		_FutureDays=parseInt(configs[ConfigIndex].toString().split(',')[3])
		}
		else
		{
		 _SelectBackDated = DisplayBackDate
		 _SelectFutureDate = DisplayFutureDate
		 _BackDays=BackDays
		 _FutureDays=FutureDays
		}
		// XML Code start here
		
		// XML Code ends here

		var	leftpos = left
		var	toppos = top
		
		if (isNaN(left))
			leftpos = -235 //-208
			
		if (isNaN(top))
			toppos = 0

		if (bPageLoaded)
		{
			if ( crossobj.visibility ==	"hidden" ) {
				ctlToPlaceValue	= ctl2
				dateFormat=format;

				formatChar = " "
				aFormat	= dateFormat.split(formatChar)
				if (aFormat.length<3)
				{
					formatChar = "/"
					aFormat	= dateFormat.split(formatChar)
					if (aFormat.length<3)
					{
						formatChar = "."
						aFormat	= dateFormat.split(formatChar)
						if (aFormat.length<3)
						{
							formatChar = "-"
							aFormat	= dateFormat.split(formatChar)
							if (aFormat.length<3)
							{
								// invalid date	format
								formatChar=""
							}
						}
					}
				}
		
				tokensChanged =	0
				if ( formatChar	!= "" )
				{
					// use user's date
					aData =	ctl2.value.split(formatChar)

					for	(i=0;i<3;i++)
					{
						if ((aFormat[i]=="d") || (aFormat[i]=="dd"))
						{
							dateSelected = parseInt(aData[i], 10)
							tokensChanged ++
						}
						else if	((aFormat[i]=="m") || (aFormat[i]=="mm"))
						{
							monthSelected =	parseInt(aData[i], 10) - 1
							tokensChanged ++
						}
						else if	(aFormat[i]=="yyyy")
						{
							yearSelected = parseInt(aData[i], 10)
							tokensChanged ++
						}
						else if	(aFormat[i]=="mmm")
						{
							for	(j=0; j<12;	j++)
							{
								if (aData[i]==monthName[j])
								{
									monthSelected=j
									tokensChanged ++
								}
							}
						}
					}
				}

				if ((tokensChanged!=3)||isNaN(dateSelected)||isNaN(monthSelected)||isNaN(yearSelected))
				{
					dateSelected = dateNow
					monthSelected =	monthNow
					yearSelected = yearNow
				}

				odateSelected=dateSelected
				omonthSelected=monthSelected
				oyearSelected=yearSelected

				aTag = ctl
				do {
					aTag = aTag.offsetParent;
					leftpos	+= aTag.offsetLeft;
					toppos += aTag.offsetTop;
				} while(aTag.tagName!="BODY");
				
				if(fixedX == -1)
				{
				    if(document.documentElement.clientWidth < ctl.offsetLeft + leftpos + 265)
				        crossobj.left =	ctl.offsetLeft + leftpos - 265
				    else
				        crossobj.left =	ctl.offsetLeft + leftpos
				}
				else
				{
				    if(document.documentElement.clientWidth < fixedX + 265)
				        crossobj.left =	fixedX - 265
				    else
				        crossobj.left =	fixedX
				}
				
				//crossobj.left =	fixedX==-1 ? ctl.offsetLeft	+ leftpos :	fixedX
				crossobj.top = fixedY==-1 ?	ctl.offsetTop +	toppos + ctl.offsetHeight +	2 :	fixedY

				constructCalendar (1, monthSelected, yearSelected);
				crossobj.visibility=(dom||ie)? "visible" : "show"

				hideElement( 'SELECT', document.getElementById("calendar") );
				hideElement( 'APPLET', document.getElementById("calendar") );			

				bShow = true;
			}
			else
			{
				hideCalendar()
				if (ctlNow!=ctl) {popUpCalendar(ctl, ctl2, format)}
			}
			ctlNow = ctl
		}
	}

	document.onkeypress = function hidecal1 () { 
		if (event.keyCode==27) 
		{
			hideCalendar()
		}
	}
	document.onclick = function hidecal2 () { 		
		if (!bShow)
		{
			hideCalendar()
		}
		bShow = false
	}

	if(ie)
	{
		init()
	}
	else
	{
		window.onload=init
	}