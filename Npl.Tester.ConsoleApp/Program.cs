using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using Npl.Resources;
using System.Resources;
namespace Npl.Tester.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {


            int run = 1;
            do
            {
                string[] consoleData;
                string resVal = "";

                Console.Write("Request : ");

                consoleData = Console.ReadLine().Split(' ');

                try
                {


                    int outInt = 0;
                    string outStr = "";
                    int outTempInt = 0;

                    foreach (var dt in consoleData)
                    {
                        if (string.IsNullOrWhiteSpace(dt))
                        {
                            continue;
                        }

                        string strDt = dt.Replace("\"", "").ToLower();
                        string resourceData = "";

                        if (Char.IsNumber(dt.ToCharArray()[0]))
                        {
                            try
                            {
                                resourceData = int.Parse(dt).ToString();
                            }
                            catch
                            {
                                resourceData = null;
                            }

                        }
                        else
                        {
                            resourceData = NumbersResources.ResourceManager.GetString(strDt);
                        }

                        if (string.IsNullOrWhiteSpace(resourceData))
                        {
                            string cntrlStr = "";
                            string cntrlRes = "";
                            int cntrlInt = 0;
                            int cntrlInt1 = 0;
                            foreach (var d in dt.ToCharArray())
                            {
                                if (Char.IsNumber(d))
                                {
                                    cntrlInt1 = d.ToString() == "0" ? cntrlInt1 * 10 : int.Parse(d.ToString());
                                    continue;
                                }

                                if (cntrlInt == 0)
                                {
                                    cntrlInt = cntrlInt1;
                                    cntrlInt1 = 0;
                                }
                                if (cntrlInt1>0)
                                {
                                    cntrlInt += cntrlInt1;
                                    cntrlInt1 = 0;
                                }
                               

                                cntrlStr += d;

                                cntrlRes = NumbersResources.ResourceManager.GetString(cntrlStr.ToLower());

                                if (cntrlRes != null)
                                {
                                    if (Char.IsNumber(cntrlRes.ToCharArray()[0]))
                                    {
                                        if (cntrlInt > 0)
                                        {
                                            cntrlInt = cntrlInt > int.Parse(cntrlRes) ? int.Parse(cntrlRes) + cntrlInt : int.Parse(cntrlRes) * cntrlInt;
                                        }
                                        else
                                        {
                                            cntrlInt = int.Parse(cntrlRes);
                                        }
                                        cntrlStr = "";
                                        cntrlRes = "";
                                    }
                                }
                            }

                            resourceData = cntrlInt > 0 ? cntrlInt.ToString() : null;

                        }


                        if (!string.IsNullOrWhiteSpace(resourceData))
                        {

                            if (outInt == 0)
                            {
                                outInt = int.Parse(resourceData);
                                continue;
                            }
                            else if (int.Parse(resourceData) > outInt)
                            {
                                outInt = outInt * int.Parse(resourceData);

                                if (outInt > 1000)
                                {
                                    outTempInt = outInt;
                                    outInt = 0;
                                }
                                continue;
                            }
                            else
                            {
                                outInt = outInt + int.Parse(resourceData);
                                continue;
                            }


                        }

                        if (outTempInt > 0)
                        {
                            outInt = outInt < outTempInt ? outInt + outTempInt : outInt * outTempInt;

                            outTempInt = 0;
                        }


                        outStr += (outInt > 0 ? outInt.ToString() : string.Empty);

                        resVal += " " + outStr + " " + dt;

                        outInt = 0;
                        outStr = "";
                    }

                    if (outTempInt > 0)
                    {
                        outInt = outInt < outTempInt ? outInt + outTempInt : outInt * outTempInt;

                        outTempInt = 0;
                    }

                    resVal += " " + (outInt > 0 ? outInt.ToString() : string.Empty);

                    Console.WriteLine("Response : " + (string.IsNullOrWhiteSpace(resVal) ? "Bulunamadı." : resVal));
                }
                catch (Exception)
                {
                    Console.WriteLine("Hatalı değer girdiniz...");
                }
            } while (run > 0);

            Console.ReadKey();

        }
    }
}
