using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Clean.Common.Service
{
	public sealed class MRZMaker
	{
		//public static string CheckDigit(string ChkDigStr, int ChkDigLen)
		//{
				
		//	ChkDigStr += new String('<',ChkDigLen - ChkDigStr.Length);

		//	string result;
		//	if (ChkDigStr.Substring(0, 1) == "<")
		//	{
		//		result = "<";
		//	}
		//	else
		//	{
		//		long fCheck = 0;
		//		MRZMaker.iMulti = 7;
		//		for (int i = 1; i <= ChkDigLen; i++)
		//		{
		//			char ss = ChkDigStr[i];
					
		//			if (ss <= '9' && ss >= '0')
		//			{
		//				fCheck += (ss - 48) * MRZMaker.iMulti;
		//			}
		//			bool flag3 = Operators.CompareString(MRZMaker.ss, "Z", false) <= 0 & Operators.CompareString(MRZMaker.ss, "A", false) >= 0;
		//			if (ss >= 'A' && ss <= 'Z')
		//			{
		//				MRZMaker.fCheck += (ss - 55) * MRZMaker.iMulti;
		//			}
		//			bool flag4 = Operators.CompareString(MRZMaker.ss, "<", false) == 0;
		//			if (flag4)
		//			{
		//				MRZMaker.fCheck += (float)(checked(0 * MRZMaker.iMulti));
		//			}
		//			int num = MRZMaker.iMulti;
		//			if (num != 1)
		//			{
		//				if (num != 3)
		//				{
		//					if (num == 7)
		//					{
		//						MRZMaker.iMulti = 3;
		//					}
		//				}
		//				else
		//				{
		//					MRZMaker.iMulti = 1;
		//				}
		//			}
		//			else
		//			{
		//				MRZMaker.iMulti = 7;
		//			}
					
		//		}
		//		MRZMaker.fCheck /= 10f;
		//		MRZMaker.iCheck = (int)Math.Round((double)(unchecked((MRZMaker.fCheck - Conversion.Int(MRZMaker.fCheck)) * 10f)));
		//		result = Strings.LTrim(Conversion.Str(MRZMaker.iCheck));
		//	}
		//	return result;
			
		//}

		//public static string LangesDatum(string dummy)
		//{
		//	string str = Strings.Left(dummy, 2) + " ";
		//	string text = Strings.Mid(dummy, 4, 2);
		//	uint num = < PrivateImplementationDetails >.ComputeStringHash(text);
		//	if (num <= 485174231u)
		//	{
		//		if (num <= 451766088u)
		//		{
		//			if (num != 418210850u)
		//			{
		//				if (num != 434988469u)
		//				{
		//					if (num == 451766088u)
		//					{
		//						if (Operators.CompareString(text, "07", false) == 0)
		//						{
		//							str += "JUL/JUIL ";
		//						}
		//					}
		//				}
		//				else if (Operators.CompareString(text, "08", false) == 0)
		//				{
		//					str += "AUG/AOÛT ";
		//				}
		//			}
		//			else if (Operators.CompareString(text, "09", false) == 0)
		//			{
		//				str += "SEP/SEPT ";
		//			}
		//		}
		//		else if (num != 468396612u)
		//		{
		//			if (num != 468543707u)
		//			{
		//				if (num == 485174231u)
		//				{
		//					if (Operators.CompareString(text, "11", false) == 0)
		//					{
		//						str += "NOV/NOV ";
		//					}
		//				}
		//			}
		//			else if (Operators.CompareString(text, "06", false) == 0)
		//			{
		//				str += "JUN/JUIN ";
		//			}
		//		}
		//		else if (Operators.CompareString(text, "10", false) == 0)
		//		{
		//			str += "OCT/OCT ";
		//		}
		//	}
		//	else if (num <= 502098945u)
		//	{
		//		if (num != 485321326u)
		//		{
		//			if (num != 501951850u)
		//			{
		//				if (num == 502098945u)
		//				{
		//					if (Operators.CompareString(text, "04", false) == 0)
		//					{
		//						str += "APR/AVR ";
		//					}
		//				}
		//			}
		//			else if (Operators.CompareString(text, "12", false) == 0)
		//			{
		//				str += "DEC/DÉC ";
		//			}
		//		}
		//		else if (Operators.CompareString(text, "05", false) == 0)
		//		{
		//			str += "MAY/MAI ";
		//		}
		//	}
		//	else if (num != 518876564u)
		//	{
		//		if (num != 535654183u)
		//		{
		//			if (num == 552431802u)
		//			{
		//				if (Operators.CompareString(text, "01", false) == 0)
		//				{
		//					str += "JAN/JAN ";
		//				}
		//			}
		//		}
		//		else if (Operators.CompareString(text, "02", false) == 0)
		//		{
		//			str += "FEB/FÉV ";
		//		}
		//	}
		//	else if (Operators.CompareString(text, "03", false) == 0)
		//	{
		//		str += "MAR/MARS ";
		//	}
		//	return str + Strings.Right(dummy, 2);
		//}

		//// Token: 0x06000910 RID: 2320 RVA: 0x0003BB7C File Offset: 0x00039D7C
		//public static string MRZ1of2(string dmyType, string dmyCountryCode, string dmySurname, string dmyGivenNames)
		//{
		//	MRZMaker.sMRL1 = Strings.StrDup(44, "<");
		//	StringType.MidStmtStr(ref MRZMaker.sMRL1, 1, 2, Strings.Trim(dmyType));
		//	StringType.MidStmtStr(ref MRZMaker.sMRL1, 3, 3, Strings.Trim(dmyCountryCode));
		//	MRZMaker.sSGN = Strings.Trim(dmySurname) + "<<" + Strings.Trim(dmyGivenNames);
		//	for (; ; )
		//	{
		//		int num = Strings.InStr(MRZMaker.sSGN, " ", CompareMethod.Binary);
		//		bool flag = num == 0;
		//		if (flag)
		//		{
		//			break;
		//		}
		//		StringType.MidStmtStr(ref MRZMaker.sSGN, num, 1, "<");
		//	}
		//	MRZMaker.sSGN = Strings.Replace(MRZMaker.sSGN, "À", "A", 1, -1, CompareMethod.Binary);
		//	MRZMaker.sSGN = Strings.Replace(MRZMaker.sSGN, "Á", "A", 1, -1, CompareMethod.Binary);
		//	MRZMaker.sSGN = Strings.Replace(MRZMaker.sSGN, "Â", "A", 1, -1, CompareMethod.Binary);
		//	MRZMaker.sSGN = Strings.Replace(MRZMaker.sSGN, "Ã", "A", 1, -1, CompareMethod.Binary);
		//	MRZMaker.sSGN = Strings.Replace(MRZMaker.sSGN, "Ä", "AE", 1, -1, CompareMethod.Binary);
		//	MRZMaker.sSGN = Strings.Replace(MRZMaker.sSGN, "Å", "A", 1, -1, CompareMethod.Binary);
		//	MRZMaker.sSGN = Strings.Replace(MRZMaker.sSGN, "Æ", "AE", 1, -1, CompareMethod.Binary);
		//	MRZMaker.sSGN = Strings.Replace(MRZMaker.sSGN, "Ç", "C", 1, -1, CompareMethod.Binary);
		//	MRZMaker.sSGN = Strings.Replace(MRZMaker.sSGN, "È", "E", 1, -1, CompareMethod.Binary);
		//	MRZMaker.sSGN = Strings.Replace(MRZMaker.sSGN, "É", "E", 1, -1, CompareMethod.Binary);
		//	MRZMaker.sSGN = Strings.Replace(MRZMaker.sSGN, "Ê", "E", 1, -1, CompareMethod.Binary);
		//	MRZMaker.sSGN = Strings.Replace(MRZMaker.sSGN, "Ë", "E", 1, -1, CompareMethod.Binary);
		//	MRZMaker.sSGN = Strings.Replace(MRZMaker.sSGN, "Ì", "I", 1, -1, CompareMethod.Binary);
		//	MRZMaker.sSGN = Strings.Replace(MRZMaker.sSGN, "Í", "I", 1, -1, CompareMethod.Binary);
		//	MRZMaker.sSGN = Strings.Replace(MRZMaker.sSGN, "Î", "I", 1, -1, CompareMethod.Binary);
		//	MRZMaker.sSGN = Strings.Replace(MRZMaker.sSGN, "Ð", "D", 1, -1, CompareMethod.Binary);
		//	MRZMaker.sSGN = Strings.Replace(MRZMaker.sSGN, "Ñ", "N", 1, -1, CompareMethod.Binary);
		//	MRZMaker.sSGN = Strings.Replace(MRZMaker.sSGN, "Ò", "O", 1, -1, CompareMethod.Binary);
		//	MRZMaker.sSGN = Strings.Replace(MRZMaker.sSGN, "Ó", "O", 1, -1, CompareMethod.Binary);
		//	MRZMaker.sSGN = Strings.Replace(MRZMaker.sSGN, "Ô", "O", 1, -1, CompareMethod.Binary);
		//	MRZMaker.sSGN = Strings.Replace(MRZMaker.sSGN, "Õ", "O", 1, -1, CompareMethod.Binary);
		//	MRZMaker.sSGN = Strings.Replace(MRZMaker.sSGN, "Ö", "OE", 1, -1, CompareMethod.Binary);
		//	MRZMaker.sSGN = Strings.Replace(MRZMaker.sSGN, "Ø", "O", 1, -1, CompareMethod.Binary);
		//	MRZMaker.sSGN = Strings.Replace(MRZMaker.sSGN, "Ù", "U", 1, -1, CompareMethod.Binary);
		//	MRZMaker.sSGN = Strings.Replace(MRZMaker.sSGN, "Ú", "U", 1, -1, CompareMethod.Binary);
		//	MRZMaker.sSGN = Strings.Replace(MRZMaker.sSGN, "Û", "U", 1, -1, CompareMethod.Binary);
		//	MRZMaker.sSGN = Strings.Replace(MRZMaker.sSGN, "Ü", "UE", 1, -1, CompareMethod.Binary);
		//	MRZMaker.sSGN = Strings.Replace(MRZMaker.sSGN, "Ý", "Y", 1, -1, CompareMethod.Binary);
		//	MRZMaker.sSGN = Strings.Replace(MRZMaker.sSGN, "Þ", "P", 1, -1, CompareMethod.Binary);
		//	MRZMaker.sSGN = Strings.Replace(MRZMaker.sSGN, "ß", "SS", 1, -1, CompareMethod.Binary);
		//	MRZMaker.sSGN = Strings.Replace(MRZMaker.sSGN, "-", "<", 1, -1, CompareMethod.Binary);
		//	int num2 = 33;
		//	checked
		//	{
		//		do
		//		{
		//			MRZMaker.sSGN = Strings.Replace(MRZMaker.sSGN, Conversions.ToString(Strings.Chr(num2)), "", 1, -1, CompareMethod.Binary);
		//			num2++;
		//		}
		//		while (num2 <= 47);
		//		int num3 = 61;
		//		do
		//		{
		//			MRZMaker.sSGN = Strings.Replace(MRZMaker.sSGN, Conversions.ToString(Strings.Chr(num3)), "", 1, -1, CompareMethod.Binary);
		//			num3++;
		//		}
		//		while (num3 <= 64);
		//		int num4 = Strings.Asc("¿");
		//		for (int i = 91; i <= num4; i++)
		//		{
		//			MRZMaker.sSGN = Strings.Replace(MRZMaker.sSGN, Conversions.ToString(Strings.Chr(i)), "", 1, -1, CompareMethod.Binary);
		//		}
		//		MRZMaker.sSGN = Strings.Replace(MRZMaker.sSGN, ":", "", 1, -1, CompareMethod.Binary);
		//		MRZMaker.sSGN = Strings.Replace(MRZMaker.sSGN, ";", "", 1, -1, CompareMethod.Binary);
		//		MRZMaker.sSGN = Strings.Replace(MRZMaker.sSGN, "×", "", 1, -1, CompareMethod.Binary);
		//		StringType.MidStmtStr(ref MRZMaker.sMRL1, 6, 39, MRZMaker.sSGN);
		//		return MRZMaker.sMRL1;
		//	}
		//}

		//// Token: 0x06000911 RID: 2321 RVA: 0x0003C084 File Offset: 0x0003A284
		//public static string MRZ2of2(string dmyNumber, string dmyCountryCode, string dmyDOB, string dmySex, string dmyDOE, string dmyPersNo)
		//{
		//	dmyNumber = dmyNumber.Trim();
		//	dmyCountryCode = dmyCountryCode.Trim();
		//	dmyDOB = dmyDOB.Substring(dmyDOB.Length - 2, 2) + dmyDOB.Substring(4, 2) +dmyDOB.Substring( 1, 2);
		//	dmySex = dmySex.Trim();
		//	dmyDOE = dmyDOE.Substring(dmyDOE.Length - 2, 2) + dmyDOE.Substring(4, 2) + dmyDOE.Substring(1, 2);
		//	string sMRL2 = Enumerable.Repeat("<", 44).Aggregate((a, b) => a + b);
			
		//	StringType.MidStmtStr(ref MRZMaker.sMRL2, 1, 9, dmyNumber);
		//	StringType.MidStmtStr(ref MRZMaker.sMRL2, 11, 3, dmyCountryCode);
		//	StringType.MidStmtStr(ref MRZMaker.sMRL2, 14, 6, dmyDOB);
		//	StringType.MidStmtStr(ref MRZMaker.sMRL2, 21, 1, dmySex);
		//	StringType.MidStmtStr(ref MRZMaker.sMRL2, 22, 6, dmyDOE);
		//	StringType.MidStmtStr(ref MRZMaker.sMRL2, 29, 14, dmyPersNo);
		//	StringType.MidStmtStr(ref MRZMaker.sMRL2, 10, 1, MRZMaker.CheckDigit(Strings.Mid(MRZMaker.sMRL2, 1, 9), 9));
		//	StringType.MidStmtStr(ref MRZMaker.sMRL2, 20, 1, MRZMaker.CheckDigit(Strings.Mid(MRZMaker.sMRL2, 14, 6), 6));
		//	StringType.MidStmtStr(ref MRZMaker.sMRL2, 28, 1, MRZMaker.CheckDigit(Strings.Mid(MRZMaker.sMRL2, 22, 6), 6));
		//	StringType.MidStmtStr(ref MRZMaker.sMRL2, 43, 1, MRZMaker.CheckDigit(Strings.Mid(MRZMaker.sMRL2, 29, 14), 14));
		//	StringType.MidStmtStr(ref MRZMaker.sMRL2, 44, 1, MRZMaker.CheckDigit(Strings.Mid(MRZMaker.sMRL2, 1, 10) + Strings.Mid(MRZMaker.sMRL2, 14, 7) + Strings.Mid(MRZMaker.sMRL2, 22, 22), 39));
		//	return MRZMaker.sMRL2;
		//}

		//// Token: 0x0400033E RID: 830
		//public static float n31X;

		//// Token: 0x0400033F RID: 831
		//public static float n31Y;

		//// Token: 0x04000340 RID: 832
		//public static float n31MRZX;

		//// Token: 0x04000341 RID: 833
		//public static float n31MRZY;

		//// Token: 0x04000342 RID: 834
		//public static float fCheck;

		//// Token: 0x04000343 RID: 835
		//public static float ppErr;

		//// Token: 0x04000344 RID: 836
		//public static int iValidityYears;

		//// Token: 0x04000345 RID: 837
		//public static int sPlaceOfIssue;

		//// Token: 0x04000346 RID: 838
		//public static int iCheck;

		//// Token: 0x04000347 RID: 839
		//public static int iMulti;

		//// Token: 0x04000348 RID: 840
		//private static string sMRL1;

		//// Token: 0x04000349 RID: 841
		//private static string sMRL2;

		//// Token: 0x0400034A RID: 842
		//private static string sSGN;

		//// Token: 0x0400034B RID: 843
		//private static string ss;

		//// Token: 0x0400034C RID: 844
		//private static string dummy;
	}
}
