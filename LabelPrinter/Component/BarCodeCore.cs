using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace LabelPrinter.Component
{
    public class BarCodeCore
    {
        public class Code128Struct
        {
            public string Value
            {
                get;
                set;
            }

            public string A
            {
                get;
                set;
            }

            public string B
            {
                get;
                set;
            }

            public string C
            {
                get;
                set;
            }

            public string Encoding
            {
                get;
                set;
            }

            public string this[string index]
            {
                get
                {
                    string tmp = string.Empty;

                    Type t = this.GetType();
                    object obj = t.GetProperty(index).GetValue(this, null);

                    if (null != obj)
                        tmp = obj.ToString();

                    return tmp;
                }
            }

            public string this[int index]
            {
                get
                {
                    string tmp = String.Empty;
                    switch (index)
                    {
                        case 0:
                            tmp = this.Value;
                            break;
                        case 1:
                            tmp = this.A;
                            break;
                        case 2:
                            tmp = this.B;
                            break;
                        case 3:
                            tmp = this.C;
                            break;
                        case 4:
                            tmp = this.Encoding;
                            break;
                    }
                    return tmp;
                }
            }
        }
        public class EnCoder128
        {
            public enum TYPES : int { DYNAMIC, A, B, C };
            private List<Code128Struct> C128_Code = new List<Code128Struct>();
            private List<string> _FormattedData = new List<string>();
            private List<string> _EncodedData = new List<string>();
            private Code128Struct StartCharacter = null;
            private TYPES type = TYPES.DYNAMIC;
            public string Raw_Data = "";

            public string Encode_Code128()
            {
                this.init_Code128();
                return GetEncoding();
            }

            private void init_Code128()
            {
                //populate data
                C128_Code.Add(new Code128Struct { Value = "0", A = " ", B = " ", C = "00", Encoding = "11011001100" });
                C128_Code.Add(new Code128Struct { Value = "1", A = "!", B = "!", C = "01", Encoding = "11001101100" });
                C128_Code.Add(new Code128Struct { Value = "2", A = "\"", B = "\"", C = "02", Encoding = "11001100110" });
                C128_Code.Add(new Code128Struct { Value = "3", A = "#", B = "#", C = "03", Encoding = "10010011000" });
                C128_Code.Add(new Code128Struct { Value = "4", A = "$", B = "$", C = "04", Encoding = "10010001100" });
                C128_Code.Add(new Code128Struct { Value = "5", A = "%", B = "%", C = "05", Encoding = "10001001100" });
                C128_Code.Add(new Code128Struct { Value = "6", A = "&", B = "&", C = "06", Encoding = "10011001000" });
                C128_Code.Add(new Code128Struct { Value = "7", A = "'", B = "'", C = "07", Encoding = "10011000100" });
                C128_Code.Add(new Code128Struct { Value = "8", A = "(", B = "(", C = "08", Encoding = "10001100100" });
                C128_Code.Add(new Code128Struct { Value = "9", A = ")", B = ")", C = "09", Encoding = "11001001000" });
                C128_Code.Add(new Code128Struct { Value = "10", A = "*", B = "*", C = "10", Encoding = "11001000100" });
                C128_Code.Add(new Code128Struct { Value = "11", A = "+", B = "+", C = "11", Encoding = "11000100100" });
                C128_Code.Add(new Code128Struct { Value = "12", A = ",", B = ",", C = "12", Encoding = "10110011100" });
                C128_Code.Add(new Code128Struct { Value = "13", A = "-", B = "-", C = "13", Encoding = "10011011100" });
                C128_Code.Add(new Code128Struct { Value = "14", A = ".", B = ".", C = "14", Encoding = "10011001110" });
                C128_Code.Add(new Code128Struct { Value = "15", A = "/", B = "/", C = "15", Encoding = "10111001100" });
                C128_Code.Add(new Code128Struct { Value = "16", A = "0", B = "0", C = "16", Encoding = "10011101100" });
                C128_Code.Add(new Code128Struct { Value = "17", A = "1", B = "1", C = "17", Encoding = "10011100110" });
                C128_Code.Add(new Code128Struct { Value = "18", A = "2", B = "2", C = "18", Encoding = "11001110010" });
                C128_Code.Add(new Code128Struct { Value = "19", A = "3", B = "3", C = "19", Encoding = "11001011100" });
                C128_Code.Add(new Code128Struct { Value = "20", A = "4", B = "4", C = "20", Encoding = "11001001110" });
                C128_Code.Add(new Code128Struct { Value = "21", A = "5", B = "5", C = "21", Encoding = "11011100100" });
                C128_Code.Add(new Code128Struct { Value = "22", A = "6", B = "6", C = "22", Encoding = "11001110100" });
                C128_Code.Add(new Code128Struct { Value = "23", A = "7", B = "7", C = "23", Encoding = "11101101110" });
                C128_Code.Add(new Code128Struct { Value = "24", A = "8", B = "8", C = "24", Encoding = "11101001100" });
                C128_Code.Add(new Code128Struct { Value = "25", A = "9", B = "9", C = "25", Encoding = "11100101100" });
                C128_Code.Add(new Code128Struct { Value = "26", A = ":", B = ":", C = "26", Encoding = "11100100110" });
                C128_Code.Add(new Code128Struct { Value = "27", A = ";", B = ";", C = "27", Encoding = "11101100100" });
                C128_Code.Add(new Code128Struct { Value = "28", A = "<", B = "<", C = "28", Encoding = "11100110100" });
                C128_Code.Add(new Code128Struct { Value = "29", A = "=", B = "=", C = "29", Encoding = "11100110010" });
                C128_Code.Add(new Code128Struct { Value = "30", A = ">", B = ">", C = "30", Encoding = "11011011000" });
                C128_Code.Add(new Code128Struct { Value = "31", A = "?", B = "?", C = "31", Encoding = "11011000110" });
                C128_Code.Add(new Code128Struct { Value = "32", A = "@", B = "@", C = "32", Encoding = "11000110110" });
                C128_Code.Add(new Code128Struct { Value = "33", A = "A", B = "A", C = "33", Encoding = "10100011000" });
                C128_Code.Add(new Code128Struct { Value = "34", A = "B", B = "B", C = "34", Encoding = "10001011000" });
                C128_Code.Add(new Code128Struct { Value = "35", A = "C", B = "C", C = "35", Encoding = "10001000110" });
                C128_Code.Add(new Code128Struct { Value = "36", A = "D", B = "D", C = "36", Encoding = "10110001000" });
                C128_Code.Add(new Code128Struct { Value = "37", A = "E", B = "E", C = "37", Encoding = "10001101000" });
                C128_Code.Add(new Code128Struct { Value = "38", A = "F", B = "F", C = "38", Encoding = "10001100010" });
                C128_Code.Add(new Code128Struct { Value = "39", A = "G", B = "G", C = "39", Encoding = "11010001000" });
                C128_Code.Add(new Code128Struct { Value = "40", A = "H", B = "H", C = "40", Encoding = "11000101000" });
                C128_Code.Add(new Code128Struct { Value = "41", A = "I", B = "I", C = "41", Encoding = "11000100010" });
                C128_Code.Add(new Code128Struct { Value = "42", A = "J", B = "J", C = "42", Encoding = "10110111000" });
                C128_Code.Add(new Code128Struct { Value = "43", A = "K", B = "K", C = "43", Encoding = "10110001110" });
                C128_Code.Add(new Code128Struct { Value = "44", A = "L", B = "L", C = "44", Encoding = "10001101110" });
                C128_Code.Add(new Code128Struct { Value = "45", A = "M", B = "M", C = "45", Encoding = "10111011000" });
                C128_Code.Add(new Code128Struct { Value = "46", A = "N", B = "N", C = "46", Encoding = "10111000110" });
                C128_Code.Add(new Code128Struct { Value = "47", A = "O", B = "O", C = "47", Encoding = "10001110110" });
                C128_Code.Add(new Code128Struct { Value = "48", A = "P", B = "P", C = "48", Encoding = "11101110110" });
                C128_Code.Add(new Code128Struct { Value = "49", A = "Q", B = "Q", C = "49", Encoding = "11010001110" });
                C128_Code.Add(new Code128Struct { Value = "50", A = "R", B = "R", C = "50", Encoding = "11000101110" });
                C128_Code.Add(new Code128Struct { Value = "51", A = "S", B = "S", C = "51", Encoding = "11011101000" });
                C128_Code.Add(new Code128Struct { Value = "52", A = "T", B = "T", C = "52", Encoding = "11011100010" });
                C128_Code.Add(new Code128Struct { Value = "53", A = "U", B = "U", C = "53", Encoding = "11011101110" });
                C128_Code.Add(new Code128Struct { Value = "54", A = "V", B = "V", C = "54", Encoding = "11101011000" });
                C128_Code.Add(new Code128Struct { Value = "55", A = "W", B = "W", C = "55", Encoding = "11101000110" });
                C128_Code.Add(new Code128Struct { Value = "56", A = "X", B = "X", C = "56", Encoding = "11100010110" });
                C128_Code.Add(new Code128Struct { Value = "57", A = "Y", B = "Y", C = "57", Encoding = "11101101000" });
                C128_Code.Add(new Code128Struct { Value = "58", A = "Z", B = "U", C = "58", Encoding = "11101100010" });
                C128_Code.Add(new Code128Struct { Value = "59", A = "[", B = "[", C = "59", Encoding = "11100011010" });
                C128_Code.Add(new Code128Struct { Value = "60", A = @"\", B = @"\", C = "60", Encoding = "11101111010" });
                C128_Code.Add(new Code128Struct { Value = "61", A = "]", B = "]", C = "61", Encoding = "11001000010" });
                C128_Code.Add(new Code128Struct { Value = "62", A = "^", B = "^", C = "62", Encoding = "11110001010" });
                C128_Code.Add(new Code128Struct { Value = "63", A = "_", B = "_", C = "63", Encoding = "10100110000" });
                C128_Code.Add(new Code128Struct { Value = "64", A = "\0", B = "`", C = "64", Encoding = "10100001100" });
                C128_Code.Add(new Code128Struct { Value = "65", A = Convert.ToChar(1).ToString(), B = "a", C = "65", Encoding = "10010110000" });
                C128_Code.Add(new Code128Struct { Value = "66", A = Convert.ToChar(2).ToString(), B = "b", C = "66", Encoding = "10010000110" });
                C128_Code.Add(new Code128Struct { Value = "67", A = Convert.ToChar(3).ToString(), B = "c", C = "67", Encoding = "10000101100" });
                C128_Code.Add(new Code128Struct { Value = "68", A = Convert.ToChar(4).ToString(), B = "d", C = "68", Encoding = "10000100110" });
                C128_Code.Add(new Code128Struct { Value = "69", A = Convert.ToChar(5).ToString(), B = "e", C = "69", Encoding = "10110010000" });
                C128_Code.Add(new Code128Struct { Value = "70", A = Convert.ToChar(6).ToString(), B = "f", C = "70", Encoding = "10110000100" });
                C128_Code.Add(new Code128Struct { Value = "71", A = Convert.ToChar(7).ToString(), B = "g", C = "71", Encoding = "10011010000" });
                C128_Code.Add(new Code128Struct { Value = "72", A = Convert.ToChar(8).ToString(), B = "h", C = "72", Encoding = "10011000010" });
                C128_Code.Add(new Code128Struct { Value = "73", A = Convert.ToChar(9).ToString(), B = "i", C = "73", Encoding = "10000110100" });
                C128_Code.Add(new Code128Struct { Value = "74", A = Convert.ToChar(10).ToString(), B = "j", C = "74", Encoding = "10000110010" });
                C128_Code.Add(new Code128Struct { Value = "75", A = Convert.ToChar(11).ToString(), B = "k", C = "75", Encoding = "11000010010" });
                C128_Code.Add(new Code128Struct { Value = "76", A = Convert.ToChar(12).ToString(), B = "l", C = "76", Encoding = "11001010000" });
                C128_Code.Add(new Code128Struct { Value = "77", A = Convert.ToChar(13).ToString(), B = "m", C = "77", Encoding = "11110111010" });
                C128_Code.Add(new Code128Struct { Value = "78", A = Convert.ToChar(14).ToString(), B = "n", C = "78", Encoding = "11000010100" });
                C128_Code.Add(new Code128Struct { Value = "79", A = Convert.ToChar(15).ToString(), B = "o", C = "79", Encoding = "10001111010" });
                C128_Code.Add(new Code128Struct { Value = "80", A = Convert.ToChar(16).ToString(), B = "p", C = "80", Encoding = "10100111100" });
                C128_Code.Add(new Code128Struct { Value = "81", A = Convert.ToChar(17).ToString(), B = "q", C = "81", Encoding = "10010111100" });
                C128_Code.Add(new Code128Struct { Value = "82", A = Convert.ToChar(18).ToString(), B = "r", C = "82", Encoding = "10010011110" });
                C128_Code.Add(new Code128Struct { Value = "83", A = Convert.ToChar(19).ToString(), B = "s", C = "83", Encoding = "10111100100" });
                C128_Code.Add(new Code128Struct { Value = "84", A = Convert.ToChar(20).ToString(), B = "t", C = "84", Encoding = "10011110100" });
                C128_Code.Add(new Code128Struct { Value = "85", A = Convert.ToChar(21).ToString(), B = "u", C = "85", Encoding = "10011110010" });
                C128_Code.Add(new Code128Struct { Value = "86", A = Convert.ToChar(22).ToString(), B = "v", C = "86", Encoding = "11110100100" });
                C128_Code.Add(new Code128Struct { Value = "87", A = Convert.ToChar(23).ToString(), B = "w", C = "87", Encoding = "11110010100" });
                C128_Code.Add(new Code128Struct { Value = "88", A = Convert.ToChar(24).ToString(), B = "x", C = "88", Encoding = "11110010010" });
                C128_Code.Add(new Code128Struct { Value = "89", A = Convert.ToChar(25).ToString(), B = "y", C = "89", Encoding = "11011011110" });
                C128_Code.Add(new Code128Struct { Value = "90", A = Convert.ToChar(26).ToString(), B = "z", C = "90", Encoding = "11011110110" });
                C128_Code.Add(new Code128Struct { Value = "91", A = Convert.ToChar(27).ToString(), B = "{", C = "91", Encoding = "11110110110" });
                C128_Code.Add(new Code128Struct { Value = "92", A = Convert.ToChar(28).ToString(), B = "|", C = "92", Encoding = "10101111000" });
                C128_Code.Add(new Code128Struct { Value = "93", A = Convert.ToChar(29).ToString(), B = "}", C = "93", Encoding = "10100011110" });
                C128_Code.Add(new Code128Struct { Value = "94", A = Convert.ToChar(30).ToString(), B = "~", C = "94", Encoding = "10001011110" });

                C128_Code.Add(new Code128Struct { Value = "95", A = Convert.ToChar(31).ToString(), B = Convert.ToChar(127).ToString(), C = "95", Encoding = "10111101000" });
                C128_Code.Add(new Code128Struct { Value = "96", A = Convert.ToChar(202).ToString()/*FNC3*/, B = Convert.ToChar(202).ToString()/*FNC3*/, C = "96", Encoding = "10111100010" });
                C128_Code.Add(new Code128Struct { Value = "97", A = Convert.ToChar(201).ToString()/*FNC2*/, B = Convert.ToChar(201).ToString()/*FNC2*/, C = "97", Encoding = "11110101000" });
                C128_Code.Add(new Code128Struct { Value = "98", A = "SHIFT", B = "SHIFT", C = "98", Encoding = "11110100010" });
                C128_Code.Add(new Code128Struct { Value = "99", A = "CODE_C", B = "CODE_C", C = "99", Encoding = "10111011110" });
                C128_Code.Add(new Code128Struct { Value = "100", A = "CODE_B", B = Convert.ToChar(203).ToString()/*FNC4*/, C = "CODE_B", Encoding = "10111101110" });
                C128_Code.Add(new Code128Struct { Value = "101", A = Convert.ToChar(203).ToString()/*FNC4*/, B = "CODE_A", C = "CODE_A", Encoding = "11101011110" });
                C128_Code.Add(new Code128Struct { Value = "102", A = Convert.ToChar(200).ToString()/*FNC1*/, B = Convert.ToChar(200).ToString()/*FNC1*/, C = Convert.ToChar(200).ToString()/*FNC1*/, Encoding = "11110101110" });
                C128_Code.Add(new Code128Struct { Value = "103", A = "START_A", B = "START_A", C = "START_A", Encoding = "11010000100" });
                C128_Code.Add(new Code128Struct { Value = "104", A = "START_B", B = "START_B", C = "START_B", Encoding = "11010010000" });
                C128_Code.Add(new Code128Struct { Value = "105", A = "START_C", B = "START_C", C = "START_C", Encoding = "11010011100" });
                C128_Code.Add(new Code128Struct { Value = "", A = "STOP", B = "STOP", C = "STOP", Encoding = "11000111010" });
            }//init_Code128

            private void BreakUpDataForEncoding()
            {
                string temp = "";
                string tempRawData = Raw_Data;

                //CODE C: adds a 0 to the front of the Raw_Data if the length is not divisible by 2
                if (this.type == TYPES.C && Raw_Data.Length % 2 > 0)
                    tempRawData = "0" + Raw_Data;

                foreach (char c in tempRawData)
                {
                    if (Char.IsNumber(c))
                    {
                        if (temp == "")
                        {
                            temp += c;
                        }//if
                        else
                        {
                            temp += c;
                            _FormattedData.Add(temp);
                            temp = "";
                        }//else
                    }//if
                    else
                    {
                        if (temp != "")
                        {
                            _FormattedData.Add(temp);
                            temp = "";
                        }//if
                        _FormattedData.Add(c.ToString());
                    }//else
                }//foreach

                //if something is still in temp go ahead and push it onto the queue
                if (temp != "")
                {
                    _FormattedData.Add(temp);
                    temp = "";
                }//if
            }

            Code128Struct GetCode128ValueByA(string A)
            {
                foreach (Code128Struct code in this.C128_Code)
                {
                    if (code.A == A)
                    {
                        return code;
                    }
                }
                return null;
            }

            Code128Struct GetCode128ValueByB(string B)
            {
                foreach (Code128Struct code in this.C128_Code)
                {
                    if (code.B == B)
                    {
                        return code;
                    }
                }
                return null;
            }

            Code128Struct GetCode128ValueByC(string C)
            {
                foreach (Code128Struct code in this.C128_Code)
                {
                    if (code.C == C)
                    {
                        return code;
                    }
                }
                return null;
            }

            Code128Struct GetCode128ByValue(string value)
            {
                foreach (Code128Struct code in this.C128_Code)
                {
                    if (code.Value == value)
                    {
                        return code;
                    }
                }
                return null;
            }

            private List<Code128Struct> FindStartorCodeCharacter(string s, ref int col)
            {
                List<Code128Struct> rows = new List<Code128Struct>();

                //if two chars are numbers then START_C or CODE_C
                if (s.Length > 1 && Char.IsNumber(s[0]) && Char.IsNumber(s[1]))
                {
                    if (StartCharacter == null)
                    {
                        StartCharacter = GetCode128ValueByA("START_C");
                        if (null != StartCharacter)
                        {
                            rows.Add(StartCharacter);
                        }
                    }//if
                    else
                    {
                        Code128Struct tmp = GetCode128ValueByA("CODE_C");
                        if (null != tmp)
                        {
                            rows.Add(tmp);
                        }
                    }

                    col = 1;
                }//if
                else
                {
                    bool AFound = false;
                    bool BFound = false;
                    foreach (Code128Struct row in this.C128_Code)
                    {
                        try
                        {
                            if (!AFound && s == row["A"].ToString())
                            {
                                AFound = true;
                                col = 2;

                                if (StartCharacter == null)
                                {
                                    StartCharacter = GetCode128ValueByA("START_A");
                                    rows.Add(StartCharacter);
                                }//if
                                else
                                {
                                    rows.Add(GetCode128ValueByB("CODE_A"));//first column is FNC4 so use B
                                }//else
                            }//if
                            else if (!BFound && s == row["B"].ToString())
                            {
                                BFound = true;
                                col = 1;

                                if (StartCharacter == null)
                                {
                                    StartCharacter = GetCode128ValueByA("START_B");
                                    rows.Add(StartCharacter);
                                }//if
                                else
                                    rows.Add(GetCode128ValueByA("CODE_B"));
                            }//else
                            else if (AFound && BFound)
                                break;
                        }//try
                        catch (Exception ex)
                        {
                            throw new Exception("EC128-1: " + ex.Message);
                        }//catch
                    }//foreach                

                    if (rows.Count <= 0)
                        throw new Exception("EC128-2: Could not determine start character.");
                }//else

                return rows;
            }

            private string CalculateCheckDigit()
            {
                string currentStartChar = _FormattedData[0];
                uint CheckSum = 0;

                for (uint i = 0; i < _FormattedData.Count; i++)
                {
                    //replace apostrophes with double apostrophes for escape chars
                    string s = _FormattedData[(int)i].Replace("'", "''");

                    //try to find value in the A column
                    Code128Struct rows = GetCode128ValueByA(s);

                    //try to find value in the B column
                    if (null == rows)
                        rows = GetCode128ValueByB(s);

                    //try to find value in the C column
                    if (null == rows)
                        rows = GetCode128ValueByC(s);

                    uint value = UInt32.Parse(rows["Value"].ToString());
                    uint addition = value * ((i == 0) ? 1 : i);
                    CheckSum += addition;
                }//for

                uint Remainder = (CheckSum % 103);
                Code128Struct RetRows = GetCode128ByValue(Remainder.ToString());
                return RetRows.Encoding;
            }

            private void InsertStartandCodeCharacters()
            {
                Code128Struct CurrentCodeSet = null;
                string CurrentCodeString = "";

                if (this.type != TYPES.DYNAMIC)
                {
                    switch (this.type)
                    {
                        case TYPES.A: _FormattedData.Insert(0, "START_A");
                            break;
                        case TYPES.B: _FormattedData.Insert(0, "START_B");
                            break;
                        case TYPES.C: _FormattedData.Insert(0, "START_C");
                            break;
                        default: throw new Exception("EC128-4: Unknown start type in fixed type encoding.");
                    }
                }//if
                else
                {
                    try
                    {
                        for (int i = 0; i < (_FormattedData.Count); i++)
                        {
                            int col = 0;
                            List<Code128Struct> tempStartChars = FindStartorCodeCharacter(_FormattedData[i], ref col);

                            if (null == tempStartChars)
                                return;

                            //check all the start characters and see if we need to stay with the same codeset or if a change of sets is required
                            bool sameCodeSet = false;
                            foreach (Code128Struct row in tempStartChars)
                            {
                                if (row["A"].ToString().EndsWith(CurrentCodeString) || row["B"].ToString().EndsWith(CurrentCodeString) || row["C"].ToString().EndsWith(CurrentCodeString))
                                {
                                    sameCodeSet = true;
                                    break;
                                }//if
                            }//foreach

                            //only insert a new code char if starting a new codeset
                            //if (CurrentCodeString == "" || !tempStartChars[0][col].ToString().EndsWith(CurrentCodeString)) /* Removed because of bug */

                            if (CurrentCodeString == "" || !sameCodeSet)
                            {
                                CurrentCodeSet = tempStartChars[0];
                                if (null == CurrentCodeSet)
                                {
                                    return;
                                }

                                bool error = true;
                                while (error)
                                {
                                    try
                                    {
                                        CurrentCodeString = CurrentCodeSet[col].ToString().Split(new char[] { '_' })[1];
                                        error = false;
                                    }//try
                                    catch
                                    {
                                        error = true;

                                        if (col++ > CurrentCodeSet.GetType().GetProperties().Length)
                                            throw new Exception("No start character found in CurrentCodeSet.");
                                    }//catch
                                }//while

                                _FormattedData.Insert(i++, CurrentCodeSet[col].ToString());
                            }//if

                        }//for
                    }//try
                    catch (Exception ex)
                    {
                        throw new Exception("EC128-3: Could not insert start and code characters.\n Message: " + ex.Message);
                    }//catch
                }//else
            }

            private string GetEncoding()
            {
                //break up data for encoding
                BreakUpDataForEncoding();

                //insert the start characters
                InsertStartandCodeCharacters();

                string CheckDigit = CalculateCheckDigit();

                string Encoded_Data = "";
                foreach (string s in _FormattedData)
                {
                    //handle exception with apostrophes in select statements
                    string s1 = s.Replace("'", "''");

                    Code128Struct E_Row = GetCode128ValueByA(s1);

                    if (E_Row == null)
                    {
                        E_Row = GetCode128ValueByB(s1);

                        if (null == E_Row)
                        {
                            E_Row = GetCode128ValueByC(s1);
                        }//if
                    }//if

                    if (null == E_Row)
                        throw new Exception("EC128-3: Could not find encoding of a value( " + s1 + " ) in the formatted data.");

                    Encoded_Data += E_Row["Encoding"].ToString();
                    _EncodedData.Add(E_Row["Encoding"].ToString());
                }//foreach

                //add the check digit
                Encoded_Data += CalculateCheckDigit();
                _EncodedData.Add(CalculateCheckDigit());

                //add the stop character
                Encoded_Data += GetCode128ValueByA("STOP")["Encoding"].ToString();
                _EncodedData.Add(GetCode128ValueByA("STOP")["Encoding"].ToString());

                //add the termination bars
                Encoded_Data += "11";
                _EncodedData.Add("11");
                return Encoded_Data;
            }
        }
        class EnCoder39
        {
            public String Raw_Data = "";
            public string Encode_Code39()
            {
                string strEncode = "010010100"; //编码初始字符
                string AlphaBet = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ-. $/+%*"; //Code39的字母
                string[] Code39 = //Code39的各字母对应码
            {    
                /* 0 */ "000110100", 
                /* 1 */ "100100001",        
                /* 2 */ "001100001", 
                /* 3 */ "101100000",
                /* 4 */ "000110001", 
                /* 5 */ "100110000", 
                /* 6 */ "001110000", 
                /* 7 */ "000100101",
                /* 8 */ "100100100",   
                /* 9 */ "001100100",  
                /* A */ "100001001",   
                /* B */ "001001001", 
                /* C */ "101001000", 
                /* D */ "000011001", 
                /* E */ "100011000",        
                /* F */ "001011000",       
                /* G */ "000001101",       
                /* H */ "100001100",        
                /* I */ "001001100",        
                /* J */ "000011100",
                /* K */ "100000011", 
                /* L */ "001000011", 
                /* M */ "101000010",       
                /* N */ "000010011",      
                /* O */ "100010010",        
                /* P */ "001010010",       
                /* Q */ "000000111", 
                /* R */ "100000110",       
                /* S */ "001000110",        
                /* T */ "000010110",       
                /* U */ "110000001",        
                /* V */ "011000001",       
                /* W */ "111000000", 
                /* X */ "010010001",       
                /* Y */ "110010000",       
                /* Z */ "011010000",      
                /* - */ "010000101",        
                /* . */ "110000100",       
                /*' '*/ "011000100",
                /* $ */ "010101000",      
                /* / */ "010100010",       
                /* + */ "010001010",        
                /* % */ "000101010",       
                /* * */ "010010100"   
            };

                Raw_Data = Raw_Data.ToUpper();
                for (int i = 0; i < Raw_Data.Length; i++)
                {
                    strEncode = string.Format("{0}0{1}", strEncode, Code39[AlphaBet.IndexOf(Raw_Data[i])]);
                }
                strEncode = string.Format("{0}0010010100", strEncode); //补上结束符号
                return strEncode;
            }
        }
        public class EnCodeString
        {
            public string code39(string RawData)
            {
                EnCoder39 coder39 = new EnCoder39();
                coder39.Raw_Data = RawData;
                return coder39.Encode_Code39();
            }

            public string code128(string RawData)
            {
                EnCoder128 coder128 = new EnCoder128();
                coder128.Raw_Data = RawData;
                return coder128.Encode_Code128();
            }
        }
        ///生成条码可视化元素
        public class EnCodeDraw
        {
            /// <summary>
            /// 生成Code39编码格式的可视化图形
            /// </summary>
            /// <param name="SourceString"></param>
            /// <returns></returns>
            public UIElement DrawImg39(string SourceString, int width, int height)
            {
                int x = 0; //左边界
                int y = 0; //上边界
                int WidLength = 2 * width; //粗BarCode长度
                int NarrowLength = 1 * width; //细BarCode长度
                int BarCodeHeight = height; //BarCode高度
                string Encoded_Value = new EnCodeString().code39(SourceString);

                int sourceLength = SourceString.Length;

                Canvas objBitmap = new Canvas();
                objBitmap.Width = ((WidLength * 3 + NarrowLength * 7) * (sourceLength + 2)) + (x * 2);
                objBitmap.Height = BarCodeHeight + (y * 2);
                objBitmap.Background = new SolidColorBrush(Colors.White);

                int intEncodeLength = Encoded_Value.Length; //编码后长度
                int intBarWidth;
                for (int i = 0; i < intEncodeLength; i++) //依码Code39 BarCode
                {
                    intBarWidth = Encoded_Value[i] == '1' ? WidLength : NarrowLength;

                    Rectangle rect = new Rectangle();
                    rect.Width = intBarWidth;
                    rect.Height = BarCodeHeight;
                    rect.Fill = i % 2 == 0 ? new SolidColorBrush(Colors.Black) : new SolidColorBrush(Colors.White);
                    rect.SetValue(Canvas.LeftProperty, Convert.ToDouble(x));
                    rect.SetValue(Canvas.TopProperty, Convert.ToDouble(y));

                    objBitmap.Children.Add(rect);

                    x += intBarWidth;
                }

                StackPanel stackPanel = new StackPanel();
                stackPanel.Orientation = Orientation.Vertical;
                stackPanel.Children.Add(objBitmap);


                TextBlock strText = new TextBlock();
                strText.FontSize = 18;
                strText.Text = SourceString;
                strText.TextAlignment = TextAlignment.Center;
                stackPanel.Children.Add(strText);

                return stackPanel;
            }

            /// <summary>
            /// 生成Code128编码格式的可视化图形
            /// </summary>
            /// <param name="SourceString"></param>
            /// <returns></returns>
            public UIElement DrawImg128(string SourceString)
            {
                string Encoded_Value = new EnCodeString().code128(SourceString);

                int iBarWidth = 1;
                int Width = iBarWidth * Encoded_Value.Length;
                int Height = 40;

                Canvas canvas = new Canvas();
                canvas.Width = Width;
                canvas.Height = Height;

                int shiftAdjustment = (Width % Encoded_Value.Length) / 2;
                //draw image
                int pos = 0;
                canvas.Background = new SolidColorBrush(Colors.White);

                while (pos < Encoded_Value.Length)
                {
                    if (Encoded_Value[pos] == '1')
                    {
                        Rectangle rect = new Rectangle();
                        rect.Fill = new SolidColorBrush(Colors.Black);
                        rect.Width = iBarWidth;
                        rect.Height = Height;
                        rect.SetValue(Canvas.LeftProperty, Convert.ToDouble(pos * iBarWidth + shiftAdjustment));
                        rect.SetValue(Canvas.TopProperty, 0.0);
                        canvas.Children.Add(rect);
                    }
                    pos++;
                }//while

                StackPanel stackpanel = new StackPanel();
                stackpanel.Orientation = Orientation.Vertical;
                stackpanel.Children.Add(canvas);

                TextBlock strText = new TextBlock();
                strText.FontSize = 12;
                strText.Text = SourceString;
                strText.TextAlignment = TextAlignment.Center;
                stackpanel.Children.Add(strText);

                return stackpanel;
            }
        }
    }
}
