using Softone;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using System;

namespace ClassLibrary8
{
    [WorksOn("ITEDOC")]
    internal class ITEDOCEvent : TXCode
    {
        public override void Initialize()
        {

        }

        public override object ExecCommand(int Cmd)
        {
            XTable ITEDOCTbl = XModule.GetTable("ITEDOC");
            
            switch (Cmd)
            {
                case 150002:
                    
                    String sql =
                       @" SELECT  SUM(ML.QTY1) AS QTY, F.TRNDATE, B.ZIP AS SHIPING_FROM, MD.SHPZIP AS SHIPING_TO
                            FROM MTRLINES ML
                            INNER JOIN FINDOC F ON F.FINDOC = ML.FINDOC
                            INNER JOIN FINDOC FF ON FF.FINDOC = ML.CCCNPRELFINDOC
                            INNER JOIN MTRDOC MD ON MD.FINDOC = FF.FINDOC
                            INNER JOIN BRANCH B ON B.BRANCH = FF.BRANCH
                            WHERE ML.FINDOC = " + ITEDOCTbl.Current["ITEDOC"].ToString() + 
                        @" AND ML.MTRUNIT = 151
                           GROUP BY F.TRNDATE, B.ZIP , MD.SHPZIP ";

                    XTable DATASql = XSupport.GetSQLDataSet(sql);

                    for (int i = 0; i < DATASql.Count; i++)
                    {
                     //   (DATASql[i, "CODE"].ToString() + " " + DATASql[i, "NAME"].ToString();
                  
                    }



                    var loginDTO = new Dictionary<string, string>
                        {
                            { "username", "user1" },
                            { "password", "123" }
                        };

                    

                    //var s1UserDTO = new Dictionary<string, string>
                    //    {
                    //        { "id", SOCARRIERTbl.Current["SOCARRIER"].ToString() },
                    //        { "email", SOCARRIERTbl.Current["EMAIL"].ToString()},
                    //        { "username", SOCARRIERTbl.Current["CCCUSERNAME"].ToString() },
                    //        { "password", SOCARRIERTbl.Current["CCCPASSWORD"].ToString() }
                    //    };

                    try
                    {
                        String accessToken = ShipInTimeRestCalls.GetAccessToken(loginDTO).GetAwaiter().GetResult();
                     //   ShipInTimeRestCalls.RegisterNewUser(s1UserDTO, accessToken).GetAwaiter().GetResult();
                        MessageBox.Show("H Διαδικασία ολοκληρώθηκε!");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("An error occurred: " + ex.Message);
                    }

                    break;

                case 150003:

                    //string inputString = "{\"COMMAND-TYPE\":\"FORM\",\"LOCATE\":{\"ID\":\"8f3bd51c-4b74-4d46-89c1-43ae8dc3e1a2\",\"SELECTION-ID\":\"" + SOCARRIERTbl.Current["SOCARRIER"].ToString() + "\"}}";
                    //byte[] inputBytes = Encoding.UTF8.GetBytes(inputString);
                    //string base64String = Convert.ToBase64String(inputBytes);
                    //String url = "http://localhost:5020/form?nav=" + base64String;
                    //Process.Start(url);

                    break;



            }
            return base.ExecCommand(Cmd);
        }
    }
}
