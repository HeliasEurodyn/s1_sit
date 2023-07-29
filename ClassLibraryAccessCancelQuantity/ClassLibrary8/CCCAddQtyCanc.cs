using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Windows.Forms;
using Softone;

namespace ClassLibrary8
{
        [WorksOn("SOCARRIER")]
    class CCCAddQtyCanc : TXCode
    {

            //AddQtyCanc addQtyCanc;

        public override void Initialize()
        {

           // String a = "";
            //addQtyCanc = new AddQtyCanc(XSupport, XModule);
            //addQtyCanc.TopLevel = false;
            //addQtyCanc.Visible = true;

            //XModule.InsertControl(addQtyCanc, "*PAGE(Page3,Αυτόματη Εισαγωγή Παραστατικών)");
        }

        public override object ExecCommand(int Cmd)
        {
            XTable SOCARRIERTbl = XModule.GetTable("SOCARRIER");

            switch (Cmd)
            {
                case 150002:

                    if (SOCARRIERTbl.Current["SOCARRIER"].ToString().Equals("")) {
                        MessageBox.Show("Δέν έχετε καταχωρήσει τον διανομέα!");
                        return base.ExecCommand(Cmd);
                    }

                    if (SOCARRIERTbl.Current["EMAIL"].ToString().Equals(""))
                    {
                        MessageBox.Show("To Εmail είναι κενό!");
                        return base.ExecCommand(Cmd);
                    }

                    if (SOCARRIERTbl.Current["CCCUSERNAME"].ToString().Equals(""))
                    {
                        MessageBox.Show("To Username είναι κενό!");
                        return base.ExecCommand(Cmd);
                    }

                    if (SOCARRIERTbl.Current["CCCPASSWORD"].ToString().Equals(""))
                    {
                        MessageBox.Show("To Password είναι κενό!");
                        return base.ExecCommand(Cmd);
                    }
             
                    var loginDTO = new Dictionary<string, string>
                        {
                            { "username", "user1" },
                            { "password", "123" }
                        };

                    var s1UserDTO = new Dictionary<string, string>
                        {
                            { "id", SOCARRIERTbl.Current["SOCARRIER"].ToString() },
                            { "email", SOCARRIERTbl.Current["EMAIL"].ToString()},
                            { "username", SOCARRIERTbl.Current["CCCUSERNAME"].ToString() },
                            { "password", SOCARRIERTbl.Current["CCCPASSWORD"].ToString() }
                        };

                    try
                    {
                        String accessToken =  ShipInTimeRestCalls.GetAccessToken(loginDTO).GetAwaiter().GetResult();
                        ShipInTimeRestCalls.RegisterNewUser(s1UserDTO, accessToken).GetAwaiter().GetResult();
                        MessageBox.Show("H Διαδικασία ολοκληρώθηκε!");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("An error occurred: " + ex.Message);
                    }

                    break;

                case 150003:

                    string inputString = "{\"COMMAND-TYPE\":\"FORM\",\"LOCATE\":{\"ID\":\"8f3bd51c-4b74-4d46-89c1-43ae8dc3e1a2\",\"SELECTION-ID\":\"" + SOCARRIERTbl.Current["SOCARRIER"].ToString() + "\"}}";
                    byte[] inputBytes = Encoding.UTF8.GetBytes(inputString);
                    string base64String = Convert.ToBase64String(inputBytes);
                    String url = "http://localhost:5020/form?nav=" + base64String;

                    Process.Start(url);

                    break;



            }
            return base.ExecCommand(Cmd);
        }



    }
}