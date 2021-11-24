using FinancialSummaryApi.V1.Boundary.Response;
using System;
using System.Text;

namespace FinancialSummaryApi.V1.UseCase.Helpers
{
    public static class TemplateGenerator
    {
        public static string GetHTMLReportString1()
        {
            var date = DateTime.Today.ToString("D");
            var sb = new StringBuilder();
            sb.Clear();
            sb.Append(@"
<html>
<head>
<meta http-equiv=,Content-Type, content=,text/html; charset=UTF-8,>

<meta name=,generator, content=,BCL easyConverter SDK 5.0.252,>
<meta name=,title, content=,Quaterly Statement Report,>
<style type=,text/css,>

body {margin-top: 0px;margin-left: 0px;}

#page_1 {position:relative; overflow: hidden;margin: 21px 0px 23px 0px;padding: 0px;border: none;width: 795px;}
#page_1 #id1_1 {border:none;margin: 0px 0px 0px 0px;padding: 0px;border:none;width: 795px;overflow: hidden;}
#page_1 #id1_2 {border:none;margin: 139px 0px 0px 325px;padding: 0px;border:none;width: 470px;overflow: hidden;}

#page_1 #p1dimg1 {position:absolute;top:17px;left:38px;z-index:-1;width:717px;height:1048px;}
#page_1 #p1dimg1 #p1img1 {width:717px;height:1048px;}




.ft0{font: 15px 'Arial';line-height: 17px;}
.ft1{font: bold 13px 'Arial Bold';color: #222222;line-height: 16px;}
.ft2{font: bold 13px 'Arial Bold';color: #222222;line-height: 14px;}
.ft3{font: bold 13px 'Arial Bold';text-decoration: underline;color: #0000ee;line-height: 16px;}
.ft4{font: 13px 'Arial';color: #222222;line-height: 16px;}
.ft5{font: 13px 'Arial';line-height: 16px;}
.ft6{font: 13px 'Arial';line-height: 14px;}
.ft7{font: 13px 'Arial';line-height: 15px;}
.ft8{font: bold 27px 'Arial Bold';line-height: 32px;}
.ft9{font: bold 14px 'Arial Bold';line-height: 16px;}
.ft10{font: bold 12px 'Arial Bold';line-height: 15px;}
.ft11{font: bold 11px 'Arial Bold';line-height: 14px;}
.ft12{font: 1px 'Arial';line-height: 1px;}
.ft13{font: 12px 'Arial';line-height: 15px;}
.ft14{font: bold 15px 'Arial Bold';line-height: 18px;}
.ft15{font: bold 10px 'Arial Bold';color: #ffffff;line-height: 14px;}

.p0{text-align: right;padding-right: 40px;margin-top: 0px;margin-bottom: 0px;}
.p1{text-align: left;padding-left: 84px;margin-top: 63px;margin-bottom: 0px;}
.p2{text-align: left;padding-left: 84px;margin-top: 0px;margin-bottom: 0px;}
.p3{text-align: left;padding-left: 487px;margin-top: 14px;margin-bottom: 0px;}
.p4{text-align: left;padding-left: 489px;margin-top: 0px;margin-bottom: 0px;}
.p5{text-align: left;padding-left: 96px;margin-top: 5px;margin-bottom: 0px;}
.p6{text-align: left;padding-left: 96px;margin-top: 0px;margin-bottom: 0px;}
.p7{text-align: left;padding-left: 164px;margin-top: 37px;margin-bottom: 0px;}
.p8{text-align: left;padding-left: 200px;margin-top: 3px;margin-bottom: 0px;}
.p9{text-align: left;padding-left: 37px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;}
.p10{text-align: left;padding-left: 57px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;}
.p11{text-align: left;padding-left: 6px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;}
.p12{text-align: left;padding-left: 20px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;}
.p13{text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;}
.p14{text-align: right;padding-right: 2px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;}
.p15{text-align: right;padding-right: 4px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;}
.p16{text-align: right;padding-right: 49px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;}
.p17{text-align: right;padding-right: 13px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;}
.p18{text-align: left;padding-left: 128px;margin-top: 0px;margin-bottom: 0px;}
.p19{text-align: justify;padding-left: 96px;padding-right: 184px;margin-top: 28px;margin-bottom: 0px;}
.p20{text-align: left;margin-top: 0px;margin-bottom: 0px;}

.td0{border-top: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 125px;vertical-align: bottom;background: #cccccc;}
.td1{border-top: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 145px;vertical-align: bottom;background: #cccccc;}
.td2{border-top: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 106px;vertical-align: bottom;background: #cccccc;}
.td3{border-top: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 74px;vertical-align: bottom;background: #cccccc;}
.td4{border-top: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 86px;vertical-align: bottom;background: #cccccc;}
.td5{border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 125px;vertical-align: bottom;background: #d9d9d9;}
.td6{border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 145px;vertical-align: bottom;background: #d9d9d9;}
.td7{border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 106px;vertical-align: bottom;background: #d9d9d9;}
.td8{border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 74px;vertical-align: bottom;background: #d9d9d9;}
.td9{border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 86px;vertical-align: bottom;background: #d9d9d9;}
.td10{border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 125px;vertical-align: bottom;}
.td11{border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 145px;vertical-align: bottom;}
.td12{border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 106px;vertical-align: bottom;}
.td13{border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 74px;vertical-align: bottom;}
.td14{border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 86px;vertical-align: bottom;}
.td15{padding: 0px;margin: 0px;width: 125px;vertical-align: bottom;background: #d9d9d9;}
.td16{padding: 0px;margin: 0px;width: 145px;vertical-align: bottom;background: #d9d9d9;}
.td17{padding: 0px;margin: 0px;width: 106px;vertical-align: bottom;background: #d9d9d9;}
.td18{padding: 0px;margin: 0px;width: 74px;vertical-align: bottom;background: #d9d9d9;}
.td19{padding: 0px;margin: 0px;width: 86px;vertical-align: bottom;background: #d9d9d9;}

.tr0{height: 23px;}
.tr1{height: 15px;}

.t0{width: 536px;margin-left: 87px;margin-top: 5px;font: 12px 'Arial';}

</style>
</head>

<body>
<div id=,page_1,>
<div id=,p1dimg1,>
<img></div>


<div id=,id1_1,>
<p class=,p0 ft0,>page 1 of 1</p>
<p class=,p1 ft1,>Income Services, Hackney Service Centre, 1 Hillman Street, London E8 1DY</p>
<p class=,p2 ft2,>Tel: 0208 356 3100 24 hour payment line: 0208 356 5050</p>
<p class=,p2 ft3,><nobr><a href=,http://www.hackney.gov.uk/your-rent,>www.hackney.gov.uk/your-rent</a></nobr></p>
<p class=,p3 ft4,>Date: 20 November 2021</p>
<p class=,p4 ft4,>payment : xxxxxxxxx</p>
<p class=,p5 ft5,>Name</p>
<p class=,p6 ft5,>Address1</p>
<p class=,p6 ft6,>Address2</p>
<p class=,p6 ft7,>Address3</p>
<p class=,p6 ft5,>postcode</p>
<p class=,p7 ft8,>STATEMENT OF YOUR ACCOUNT</p>
<p class=,p8 ft9,>for the period 20 August 2021 to 20 November 2021</p>
<table cellpadding=0 cellspacing=0 class=,t0,>
<tr>
	<td class=,tr0 td0,><p class=,p9 ft10,>Date</p></td>
	<td class=,tr0 td1,><p class=,p9 ft11,>transaction Details</p></td>
	<td class=,tr0 td2,><p class=,p10 ft10,>Debit</p></td>
	<td class=,tr0 td3,><p class=,p11 ft10,>Credit</p></td>
	<td class=,tr0 td4,><p class=,p12 ft10,>Balance</p></td>
</tr>
<tr>
	<td class=,tr1 td5,><p class=,p13 ft12,>&nbsp;</p></td>
	<td class=,tr1 td6,><p class=,p13 ft13,>Balance brought forward</p></td>
	<td class=,tr1 td7,><p class=,p13 ft12,>&nbsp;</p></td>
	<td class=,tr1 td8,><p class=,p13 ft12,>&nbsp;</p></td>
	<td class=,tr1 td9,><p class=,p14 ft10,>£472.00</p></td>
</tr>
<tr>
	<td class=,tr1 td10,><p class=,p15 ft13,>10 Sep 2021</p></td>
	<td class=,tr1 td11,><p class=,p13 ft13,>Estate</p></td>
	<td class=,tr1 td12,><p class=,p16 ft13,>£379.00</p></td>
	<td class=,tr1 td13,><p class=,p17 ft13,>£117.00</p></td>
	<td class=,tr1 td14,><p class=,p14 ft10,>£1,501.00</p></td>
</tr>
<tr>
	<td class=,tr1 td5,><p class=,p15 ft13,>01 Nov2021</p></td>
	<td class=,tr1 td6,><p class=,p13 ft13,>Block</p></td>
	<td class=,tr1 td7,><p class=,p16 ft13,>£748.00</p></td>
	<td class=,tr1 td8,><p class=,p17 ft13,>£799.00</p></td>
	<td class=,tr1 td9,><p class=,p14 ft10,>£1,410.00</p></td>
</tr>
<tr>
	<td class=,tr1 td10,><p class=,p15 ft13,>17 Sep 2021</p></td>
	<td class=,tr1 td11,><p class=,p13 ft13,>Block</p></td>
	<td class=,tr1 td12,><p class=,p16 ft13,>£599.00</p></td>
	<td class=,tr1 td13,><p class=,p17 ft13,>£659.00</p></td>
	<td class=,tr1 td14,><p class=,p14 ft10,>£481.00</p></td>
</tr>
<tr>
	<td class=,tr1 td5,><p class=,p15 ft13,>30 Sep 2021</p></td>
	<td class=,tr1 td6,><p class=,p13 ft13,>Estate</p></td>
	<td class=,tr1 td7,><p class=,p16 ft13,>£877.00</p></td>
	<td class=,tr1 td8,><p class=,p17 ft13,>£999.00</p></td>
	<td class=,tr1 td9,><p class=,p14 ft10,>£895.00</p></td>
</tr>
<tr>
	<td class=,tr1 td10,><p class=,p15 ft13,>18 Sep 2021</p></td>
	<td class=,tr1 td11,><p class=,p13 ft13,>Block</p></td>
	<td class=,tr1 td12,><p class=,p16 ft13,>£555.00</p></td>
	<td class=,tr1 td13,><p class=,p17 ft13,>£337.00</p></td>
	<td class=,tr1 td14,><p class=,p14 ft10,>£448.00</p></td>
</tr>
<tr>
	<td class=,tr1 td5,><p class=,p15 ft13,>12 Oct 2021</p></td>
	<td class=,tr1 td6,><p class=,p13 ft13,>Block</p></td>
	<td class=,tr1 td7,><p class=,p16 ft13,>£786.00</p></td>
	<td class=,tr1 td8,><p class=,p17 ft13,>£941.00</p></td>
	<td class=,tr1 td9,><p class=,p14 ft10,>£1,935.00</p></td>
</tr>
<tr>
	<td class=,tr1 td10,><p class=,p15 ft13,>13 Oct 2021</p></td>
	<td class=,tr1 td11,><p class=,p13 ft13,>Block</p></td>
	<td class=,tr1 td12,><p class=,p16 ft13,>£945.00</p></td>
	<td class=,tr1 td13,><p class=,p17 ft13,>£934.00</p></td>
	<td class=,tr1 td14,><p class=,p14 ft10,>£401.00</p></td>
</tr>
<tr>
	<td class=,tr1 td5,><p class=,p15 ft13,>05 Nov2021</p></td>
	<td class=,tr1 td6,><p class=,p13 ft13,>Block</p></td>
	<td class=,tr1 td7,><p class=,p16 ft13,>£877.00</p></td>
	<td class=,tr1 td8,><p class=,p17 ft13,>£654.00</p></td>
	<td class=,tr1 td9,><p class=,p14 ft10,>£1,962.00</p></td>
</tr>
<tr>
	<td class=,tr1 td10,><p class=,p15 ft13,>18 Sep 2021</p></td>
	<td class=,tr1 td11,><p class=,p13 ft13,>Block</p></td>
	<td class=,tr1 td12,><p class=,p16 ft13,>£673.00</p></td>
	<td class=,tr1 td13,><p class=,p17 ft13,>£592.00</p></td>
	<td class=,tr1 td14,><p class=,p14 ft10,>£888.00</p></td>
</tr>
<tr>
	<td class=,tr1 td5,><p class=,p15 ft13,>24 Oct 2021</p></td>
	<td class=,tr1 td6,><p class=,p13 ft13,>Estate</p></td>
	<td class=,tr1 td7,><p class=,p16 ft13,>£277.00</p></td>
	<td class=,tr1 td8,><p class=,p17 ft13,>£128.00</p></td>
	<td class=,tr1 td9,><p class=,p14 ft10,>£704.00</p></td>
</tr>
<tr>
	<td class=,tr1 td10,><p class=,p15 ft13,>17 Sep 2021</p></td>
	<td class=,tr1 td11,><p class=,p13 ft13,>Estate</p></td>
	<td class=,tr1 td12,><p class=,p16 ft13,>£435.00</p></td>
	<td class=,tr1 td13,><p class=,p17 ft13,>£469.00</p></td>
	<td class=,tr1 td14,><p class=,p14 ft10,>£1,822.00</p></td>
</tr>
<tr>
	<td class=,tr1 td5,><p class=,p15 ft13,>25 Oct 2021</p></td>
	<td class=,tr1 td6,><p class=,p13 ft13,>Estate</p></td>
	<td class=,tr1 td7,><p class=,p16 ft13,>£859.00</p></td>
	<td class=,tr1 td8,><p class=,p17 ft13,>£434.00</p></td>
	<td class=,tr1 td9,><p class=,p14 ft10,>£1,377.00</p></td>
</tr>
<tr>
	<td class=,tr1 td10,><p class=,p15 ft13,>02 Nov2021</p></td>
	<td class=,tr1 td11,><p class=,p13 ft13,>Estate</p></td>
	<td class=,tr1 td12,><p class=,p16 ft13,>£966.00</p></td>
	<td class=,tr1 td13,><p class=,p17 ft13,>£585.00</p></td>
	<td class=,tr1 td14,><p class=,p14 ft10,>£1,399.00</p></td>
</tr>
<tr>
	<td class=,tr1 td5,><p class=,p15 ft13,>25 Aug 2021</p></td>
	<td class=,tr1 td6,><p class=,p13 ft13,>Block</p></td>
	<td class=,tr1 td7,><p class=,p16 ft13,>£528.00</p></td>
	<td class=,tr1 td8,><p class=,p17 ft13,>£229.00</p></td>
	<td class=,tr1 td9,><p class=,p14 ft10,>£1,670.00</p></td>
</tr>
<tr>
	<td class=,tr1 td10,><p class=,p15 ft13,>24 Sep 2021</p></td>
	<td class=,tr1 td11,><p class=,p13 ft13,>Estate</p></td>
	<td class=,tr1 td12,><p class=,p16 ft13,>£167.00</p></td>
	<td class=,tr1 td13,><p class=,p17 ft13,>£240.00</p></td>
	<td class=,tr1 td14,><p class=,p14 ft10,>£1,179.00</p></td>
</tr>
<tr>
	<td class=,tr1 td5,><p class=,p15 ft13,>01 Sep 2021</p></td>
	<td class=,tr1 td6,><p class=,p13 ft13,>Block</p></td>
	<td class=,tr1 td7,><p class=,p16 ft13,>£194.00</p></td>
	<td class=,tr1 td8,><p class=,p17 ft13,>£869.00</p></td>
	<td class=,tr1 td9,><p class=,p14 ft10,>£440.00</p></td>
</tr>
<tr>
	<td class=,tr1 td10,><p class=,p15 ft13,>15 Nov2021</p></td>
	<td class=,tr1 td11,><p class=,p13 ft13,>Block</p></td>
	<td class=,tr1 td12,><p class=,p16 ft13,>£419.00</p></td>
	<td class=,tr1 td13,><p class=,p17 ft13,>£411.00</p></td>
	<td class=,tr1 td14,><p class=,p14 ft10,>£532.00</p></td>
</tr>
<tr>
	<td class=,tr1 td5,><p class=,p15 ft13,>27 Oct 2021</p></td>
	<td class=,tr1 td6,><p class=,p13 ft13,>Block</p></td>
	<td class=,tr1 td7,><p class=,p16 ft13,>£852.00</p></td>
	<td class=,tr1 td8,><p class=,p17 ft13,>£257.00</p></td>
	<td class=,tr1 td9,><p class=,p14 ft10,>£706.00</p></td>
</tr>
<tr>
	<td class=,tr1 td10,><p class=,p15 ft13,>19 Sep 2021</p></td>
	<td class=,tr1 td11,><p class=,p13 ft13,>Estate</p></td>
	<td class=,tr1 td12,><p class=,p16 ft13,>£266.00</p></td>
	<td class=,tr1 td13,><p class=,p17 ft13,>£728.00</p></td>
	<td class=,tr1 td14,><p class=,p14 ft10,>£1,824.00</p></td>
</tr>
<tr>
	<td class=,tr1 td5,><p class=,p15 ft13,>26 Aug 2021</p></td>
	<td class=,tr1 td6,><p class=,p13 ft13,>Estate</p></td>
	<td class=,tr1 td7,><p class=,p16 ft13,>£295.00</p></td>
	<td class=,tr1 td8,><p class=,p17 ft13,>£352.00</p></td>
	<td class=,tr1 td9,><p class=,p14 ft10,>£1,892.00</p></td>
</tr>
<tr>
	<td class=,tr1 td10,><p class=,p15 ft13,>17 Oct 2021</p></td>
	<td class=,tr1 td11,><p class=,p13 ft13,>Block</p></td>
	<td class=,tr1 td12,><p class=,p16 ft13,>£418.00</p></td>
	<td class=,tr1 td13,><p class=,p17 ft13,>£693.00</p></td>
	<td class=,tr1 td14,><p class=,p14 ft10,>£501.00</p></td>
</tr>
<tr>
	<td class=,tr1 td5,><p class=,p15 ft13,>22 Oct 2021</p></td>
	<td class=,tr1 td6,><p class=,p13 ft13,>Estate</p></td>
	<td class=,tr1 td7,><p class=,p16 ft13,>£473.00</p></td>
	<td class=,tr1 td8,><p class=,p17 ft13,>£411.00</p></td>
	<td class=,tr1 td9,><p class=,p14 ft10,>£1,688.00</p></td>
</tr>
<tr>
	<td class=,tr1 td10,><p class=,p15 ft13,>30 Oct 2021</p></td>
	<td class=,tr1 td11,><p class=,p13 ft13,>Estate</p></td>
	<td class=,tr1 td12,><p class=,p16 ft13,>£522.00</p></td>
	<td class=,tr1 td13,><p class=,p17 ft13,>£836.00</p></td>
	<td class=,tr1 td14,><p class=,p14 ft10,>£563.00</p></td>
</tr>
<tr>
	<td class=,tr1 td5,><p class=,p15 ft13,>08 Sep 2021</p></td>
	<td class=,tr1 td6,><p class=,p13 ft13,>Estate</p></td>
	<td class=,tr1 td7,><p class=,p16 ft13,>£842.00</p></td>
	<td class=,tr1 td8,><p class=,p17 ft13,>£728.00</p></td>
	<td class=,tr1 td9,><p class=,p14 ft10,>£1,999.00</p></td>
</tr>
<tr>
	<td class=,tr1 td10,><p class=,p15 ft13,>30 Sep 2021</p></td>
	<td class=,tr1 td11,><p class=,p13 ft13,>Block</p></td>
	<td class=,tr1 td12,><p class=,p16 ft13,>£438.00</p></td>
	<td class=,tr1 td13,><p class=,p17 ft13,>£473.00</p></td>
	<td class=,tr1 td14,><p class=,p14 ft10,>£1,688.00</p></td>
</tr>
<tr>
	<td class=,tr1 td5,><p class=,p15 ft13,>02 Nov2021</p></td>
	<td class=,tr1 td6,><p class=,p13 ft13,>Block</p></td>
	<td class=,tr1 td7,><p class=,p16 ft13,>£792.00</p></td>
	<td class=,tr1 td8,><p class=,p17 ft13,>£369.00</p></td>
	<td class=,tr1 td9,><p class=,p14 ft10,>£1,426.00</p></td>
</tr>
<tr>
	<td class=,tr1 td10,><p class=,p15 ft13,>08 Oct 2021</p></td>
	<td class=,tr1 td11,><p class=,p13 ft13,>Block</p></td>
	<td class=,tr1 td12,><p class=,p16 ft13,>£488.00</p></td>
	<td class=,tr1 td13,><p class=,p17 ft13,>£176.00</p></td>
	<td class=,tr1 td14,><p class=,p14 ft10,>£543.00</p></td>
</tr>
<tr>
	<td class=,tr1 td15,><p class=,p15 ft13,>15 Sep 2021</p></td>
	<td class=,tr1 td16,><p class=,p13 ft13,>Block</p></td>
	<td class=,tr1 td17,><p class=,p16 ft13,>£746.00</p></td>
	<td class=,tr1 td18,><p class=,p17 ft13,>£567.00</p></td>
	<td class=,tr1 td19,><p class=,p14 ft10,>£617.00</p></td>
</tr>
</table>
<p class=,p18 ft14,>As of 20 November 2021 your account balance was £617.00 in arrears.</p>
<p class=,p19 ft15,>As your landlord, the council has a duty to make sure all charges are paid up to date. This is because the housing income goes toward the upkeep of council housing and providing services for residents. You must make weekly charges payment a priority. If you don’t pay, you risk losing your home.</p>
</div>
<div id=,id1_2,>
<p class=,p20 ft0,>Quaterly Statement Report</p>
</div>
</div>
</body>
</html>
");

            return sb.ToString();
        }
        public static string GetHTMLReportString(ExportResponse report)
        {
            var date = DateTime.Today.ToString("D");
            var sb = new StringBuilder();
            sb.Clear();
            sb.Append(@"<html><head>
<style>
body {
    margin-top: 0px;
    margin-left: 0px;
}

#page_1 {
    position: relative;
    overflow: hidden;
    margin: 15px 0px 35px 53px;
    padding: 0px;
    border: none;
    width: 800px;
    height: auto;
}

    #page_1 #id1_1 {
        border: none;
        margin: 79px 0px 0px 4px;
        padding: 0px;
        border: none;
        width: 654px;
    }

    #page_1 #id1_2 {
        margin: 39px 0px 0px 19px;
        padding: 0px;
        border: none;
        width: 652px;
        background: #000000;
        color: #fff;
    }

    #page_1 #p1dimg1 {
        position: absolute;
        top: 0px;
        left: 0px;
        z-index: -1;
        width: auto;
        height: auto;
    }

        #page_1 #p1dimg1 #p1img1 {
            width: auto;
            height: auto;
        }




#page_2 {
    position: relative;
    overflow: hidden;
    margin: 1123px 0px 0px 0px;
    padding: 0px;
    border: none;
    width: 0px;
    height: 0px;
}





.dclr {
    clear: both;
    float: none;
    height: 1px;
    margin: 0px;
    padding: 0px;
    overflow: hidden;
}

.ft0 {
    font: bold 13px 'Arial';
    color: #222222;
    line-height: 16px;
}

.ft1 {
    font: bold 13px 'Arial';
    line-height: 16px;
}

.ft2 {
    font: 13px 'Arial';
    color: #222222;
    line-height: 16px;
}

.ft3 {
    font: 13px 'Arial';
    line-height: 16px;
}

.ft4 {
    font: bold 29px 'Arial';
    line-height: 34px;
}

.ft5 {
    font: bold 15px 'Arial';
    line-height: 18px;
}

.ft6 {
    font: bold 12px 'Arial';
    line-height: 15px;
}

.ft7 {
    font: 1px 'Arial';
    line-height: 1px;
}

.ft8 {
    font: 12px 'Arial';
    line-height: 15px;
}

.ft9 {
    font: 1px 'Arial';
    line-height: 4px;
}

.ft10 {
    font: bold 17px 'Arial';
    line-height: 19px;
}

.ft11 {
    font: bold 12px 'Arial';
    color: #ffffff;
    line-height: 15px;
}

.p0 {
    text-align: left;
    margin-top: 0px;
    margin-bottom: 0px;
}

.p1 {
    text-align: left;
    margin-top: 1px;
    margin-bottom: 0px;
}

.p2 {
    text-align: left;
    padding-left: 504px;
    margin-top: 3px;
    margin-bottom: 0px;
}

.p3 {
    text-align: left;
    padding-left: 507px;
    margin-top: 2px;
    margin-bottom: 0px;
}

.p4 {
    text-align: left;
    padding-left: 13px;
    margin-top: 10px;
    margin-bottom: 0px;
}

.p5 {
    text-align: left;
    padding-left: 13px;
    margin-top: 2px;
    margin-bottom: 0px;
}

.p6 {
    text-align: left;
    padding-left: 100px;
    margin-top: 24px;
    margin-bottom: 0px;
}

.p7 {
    text-align: left;
    padding-left: 145px;
    margin-top: 7px;
    margin-bottom: 0px;
}

.p8 {
    text-align: left;
    padding-left: 45px;
    margin-top: 0px;
    margin-bottom: 0px;
    white-space: nowrap;
}

.p9 {
    text-align: left;
    padding-left: 49px;
    margin-top: 0px;
    margin-bottom: 0px;
    white-space: nowrap;
}

.p10 {
    text-align: left;
    padding-left: 46px;
    margin-top: 0px;
    margin-bottom: 0px;
    white-space: nowrap;
}

.p11 {
    text-align: left;
    margin-top: 0px;
    margin-bottom: 0px;
    white-space: nowrap;
}

.p12 {
    text-align: left;
    padding-left: 34px;
    margin-top: 0px;
    margin-bottom: 0px;
    white-space: nowrap;
}

.p13 {
    text-align: left;
    padding-left: 37px;
    margin-top: 0px;
    margin-bottom: 0px;
    white-space: nowrap;
}

.p14 {
    text-align: left;
    padding-left: 1px;
    margin-top: 0px;
    margin-bottom: 0px;
    white-space: nowrap;
}

.p15 {
    text-align: right;
    padding-right: 2px;
    margin-top: 0px;
    margin-bottom: 0px;
    white-space: nowrap;
}

.p16 {
    text-align: right;
    padding-right: 1px;
    margin-top: 0px;
    margin-bottom: 0px;
    white-space: nowrap;
}

.p17 {
    text-align: left;
    padding-left: 3px;
    margin-top: 0px;
    margin-bottom: 0px;
    white-space: nowrap;
}

.p18 {
    text-align: right;
    padding-right: 3px;
    margin-top: 0px;
    margin-bottom: 0px;
    white-space: nowrap;
}

.p19 {
    text-align: right;
    margin-top: 0px;
    margin-bottom: 0px;
    white-space: nowrap;
}

.p20 {
    text-align: left;
    padding-left: 56px;
    margin-top: 2px;
    margin-bottom: 0px;
}

.td0 {
    border-left: #000000 1px solid;
    border-right: #000000 1px solid;
    border-top: #000000 1px solid;
    border-bottom: #cccccc 1px solid;
    padding: 0px;
    margin: 0px;
    width: 113px;
    vertical-align: bottom;
    background: #cccccc;
}

.td1 {
    border-right: #000000 1px solid;
    border-top: #000000 1px solid;
    border-bottom: #cccccc 1px solid;
    padding: 0px;
    margin: 0px;
    width: 207px;
    vertical-align: bottom;
    background: #cccccc;
}

.td2 {
    border-top: #000000 1px solid;
    border-bottom: #cccccc 1px solid;
    padding: 0px;
    margin: 0px;
    width: 79px;
    vertical-align: bottom;
    background: #cccccc;
}

.td3 {
    border-right: #000000 1px solid;
    border-top: #000000 1px solid;
    border-bottom: #cccccc 1px solid;
    padding: 0px;
    margin: 0px;
    width: 43px;
    vertical-align: bottom;
    background: #cccccc;
}

.td4 {
    border-right: #000000 1px solid;
    border-top: #000000 1px solid;
    border-bottom: #cccccc 1px solid;
    padding: 0px;
    margin: 0px;
    width: 104px;
    vertical-align: bottom;
    background: #cccccc;
}

.td5 {
    border-right: #000000 1px solid;
    border-top: #000000 1px solid;
    border-bottom: #cccccc 1px solid;
    padding: 0px;
    margin: 0px;
    width: 119px;
    vertical-align: bottom;
    background: #cccccc;
}

.td6 {
    border-left: #000000 1px solid;
    border-top: #000000 1px solid;
    padding: 0px;
    margin: 0px;
    width: 60px;
    vertical-align: bottom;
}

.td7 {
    border-right: #000000 1px solid;
    border-top: #000000 1px solid;
    padding: 0px;
    margin: 0px;
    width: 53px;
    vertical-align: bottom;
}

.td8 {
    border-right: #000000 1px solid;
    border-top: #000000 1px solid;
    padding: 0px;
    margin: 0px;
    width: 207px;
    vertical-align: bottom;
}

.td9 {
    border-top: #000000 1px solid;
    padding: 0px;
    margin: 0px;
    width: 79px;
    vertical-align: bottom;
}

.td10 {
    border-right: #000000 1px solid;
    border-top: #000000 1px solid;
    padding: 0px;
    margin: 0px;
    width: 43px;
    vertical-align: bottom;
}

.td11 {
    border-right: #000000 1px solid;
    border-top: #000000 1px solid;
    padding: 0px;
    margin: 0px;
    width: 104px;
    vertical-align: bottom;
}

.td12 {
    border-right: #000000 1px solid;
    border-top: #000000 1px solid;
    padding: 0px;
    margin: 0px;
    width: 119px;
    vertical-align: bottom;
}

.td13 {
    border-left: #000000 1px solid;
    border-right: #000000 1px solid;
    border-bottom: #000000 1px solid;
    padding: 0px;
    margin: 0px;
    width: 113px;
    vertical-align: bottom;
}

.td14 {
    border-right: #000000 1px solid;
    border-bottom: #000000 1px solid;
    padding: 0px;
    margin: 0px;
    width: 207px;
    vertical-align: bottom;
}

.td15 {
    border-bottom: #000000 1px solid;
    padding: 0px;
    margin: 0px;
    width: 79px;
    vertical-align: bottom;
}

.td16 {
    border-right: #000000 1px solid;
    border-bottom: #000000 1px solid;
    padding: 0px;
    margin: 0px;
    width: 43px;
    vertical-align: bottom;
}

.td17 {
    border-right: #000000 1px solid;
    border-bottom: #000000 1px solid;
    padding: 0px;
    margin: 0px;
    width: 104px;
    vertical-align: bottom;
}

.td18 {
    border-right: #000000 1px solid;
    border-bottom: #000000 1px solid;
    padding: 0px;
    margin: 0px;
    width: 119px;
    vertical-align: bottom;
}

.td19 {
    border-left: #000000 1px solid;
    border-right: #000000 1px solid;
    border-bottom: #d9d9d9 1px solid;
    padding: 0px;
    margin: 0px;
    width: 113px;
    vertical-align: bottom;
    background: #d9d9d9;
}


.tr0 {
    height: 23px;
}

.tr1 {
    height: 20px;
}

.tr2 {
    height: 4px;
}

.tr3 {
    height: 21px;
}

.tr4 {
    height: 19px;
}

.t0 {
    width: 670px;
    margin-left: 4px;
    margin-top: 13px;
    font: 12px 'Arial';
}

td, th {
    border: #000000 1px solid !important;
    border-bottom: #d9d9d9 1px solid !important;
    padding: 0px;
    margin: 0px;
    vertical-align: bottom;
}

p {
    display: block;
    margin-block-start: 1em;
    margin-block-end: 1em;
    margin-inline-start: 0px;
    margin-inline-end: 0px;
}

div {
    display: block;
}

.address {
    border: 2px solid gray;
    width: 250px;
    height: 130px;
}

tr:nth-child(even) {
    background: #d9d9d9;
}

</style>
</head>
            <body>
    <div id='page_1'>
    <div id='p1dimg1'>
    <img src='data:image/png;base64,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' id='p1img1'></div>


            <div class='dclr'></div>
            <div id='id1_1'>
            <p class='p0 ft0'>Income Services, Hackney Service Centre, 1 Hillman Street, London E8 1DY</p>
            <p class='p1 ft0'>Tel: 0208 356 3100 24 hour payment line: 0208 356 5050</p>
            <p class='p0 ft1'><nobr><a href='http://www.hackney.gov.uk/your-rent'>www.hackney.gov.uk/your-rent</a></nobr><hr></p>");
            sb.AppendFormat(@"
            <p class='p2 ft2'>Date: {0}</p>
            <p class='p3 ft2'>Payment : xxxxxxxxx</p>
            <div class='address'>
            <p class='p4 ft3'>Name</p>
            <p class='p5 ft3'>Address1</p>
            <p class='p5 ft3'>Address2</p>
            <p class='p5 ft3'>Address3</p>
            <p class='p5 ft3'>Postcode</p>
            </div>
            <p class='p6 ft4'>STATEMENT OF YOUR ACCOUNT</p>
            <p class='p7 ft5'>for the period {1}</p>
            <table cellpadding='0' cellspacing='0' class='t0'>
            <tbody>
            <tr>
	            <td colspan='2' class='tr0 td0'><p class='p8 ft6'>Date</p></td>
	            <td class='tr0 td1'><p class='p9 ft6'>Transaction Details</p></td>
	            <td class='tr0 td2'><p class='p10 ft6'>Debit</p></td>
	            <td class='tr0 td4'><p class='p12 ft6'>Credit</p></td>
	            <td class='tr0 td5'><p class='p13 ft6'>Balance</p></td>
            </tr>
            <tr>
	            <td colspan='2' class='tr1 td6'><p class='p16 ft8'>&nbsp;</p></td>
	            <td class='tr1 td8'><p class='p17 ft8'>Balance brought forward</p></td>
	            <td class='tr1 td9'><p class='p11 ft7'>&nbsp;</p></td>
	            <td class='tr1 td11'><p class='p15 ft8'>&nbsp;</p></td>
	            <td class='tr1 td12'><p class='p15 ft6'>{2}</p></td>
            </tr>", date, report.StatementPeriod, report.BalanceBroughtForward);
            foreach (var item in report.Data)
            {
                sb.AppendFormat(@"
				            <tr>
	            <td colspan='2' class='tr1 td25'><p class='p16 ft8'>{0}</p></td>
	            <td class='tr1 td8'><p class='p17 ft8'>{1}</p></td>
	            <td class='tr1 td10'><p class='p11 ft8'>{2}</p></td>
	            <td class='tr1 td11'><p class='p15 ft8'><nobr>{3}</nobr></p></td>
	            <td class='tr1 td12'><p class='p15 ft6'>{4}</p></td>
            </tr>", item.Date, item.TransactionDetail, item.Debit, item.Credit, item.Balance);
            };

            sb.AppendFormat(@"
            </tbody>
            </table>
            <p class='p20 ft10'>As of {0} your account balance was {1} in arrears.</p>
            </div>", date, report.Balance);
            sb.Append(@"
            <div id='id1_2'>
            <p class='p0 ft11'>As your landlord, the council has a duty to make sure all charges are paid up to date. This is because the housing income goes toward the upkeep of council housing and providing services for residents. You must make weekly charges payment a priority. If you don’t pay, you risk losing your home.</p>
            </div>
            </div>

            </body></html>");

            return sb.ToString();
        }
    }
}
