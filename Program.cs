using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsoleApplication
{
    public class Delivery
    {
        private double distance;
        //{
        //    get
        //    {
        //        return distance;
        //    }
        //    set
        //    {
        //        distance = value;
        //    }
        //}
        private string obj_name, car_sign;
        private DateTime time_delivery;

        public Delivery()
        {
            distance = 0.0;
            obj_name = "";
            car_sign = "";
            time_delivery = DateTime.Now;
        }

        public Delivery(double _dist, string _obj_name, string _car_sign, DateTime _time)
        {
            distance = _dist;
            obj_name = _obj_name;
            car_sign = _car_sign;
            time_delivery = _time;
        }

        public Delivery(Delivery delivery)
        {
            this.distance = delivery.distance;
            this.obj_name = delivery.obj_name;
            this.car_sign = delivery.car_sign;
            this.time_delivery = delivery.time_delivery;
        }

        public void print()
        {
            Console.WriteLine($"\t\t\tDistance is {distance}, Object Name is {obj_name}, Date and Time of Delivery is {time_delivery} and Car Sign is {car_sign}.\n");
        }

        public DateTime getTimeDelivery() { return time_delivery; }
    }
    public class Program
    {
        static void Main(string[] args)
        {
            List<Delivery> deliveryList = new List<Delivery>();
            do
            {
                try
                {
                    string usr_inp, s;
                    Console.WriteLine("\t\t\t1. Input Information \n\t\t\t2. Sort by user keyword \n\t\t\t3. Exit");
                    usr_inp = Console.ReadLine();
                    switch (int.Parse(usr_inp))
                    {
                        case 1:
                            {
                                StreamReader fileStream = new StreamReader("input.txt");
                                while (!fileStream.EndOfStream)
                                {
                                    s = fileStream.ReadLine();
                                    string[] vs = s.Split(new char[] { ' ' });
                                    string[] datetime = vs[3].Split(new char[] { ',' });
                                    DateTime dt = new DateTime(int.Parse(datetime[0]),int.Parse(datetime[1]),int.Parse(datetime[2]));
                                    deliveryList.Add(new Delivery(double.Parse(vs[0]), vs[1], vs[2], dt));
                                    //Console.WriteLine(deliveryList.Count());
                                }
                            }
                            break;
                        case 2:
                            {
                                Console.WriteLine("\t\t\tInput your date (format: year,month,day)");
                                usr_inp = Console.ReadLine();
                                string[] datetime = usr_inp.Split(new char[] { ',', ' ' });
                                DateTime dt = new DateTime(int.Parse(datetime[0]), int.Parse(datetime[1]), int.Parse(datetime[2]));
                                //Console.WriteLine(deliveryList.Count());
                                for (int i = 0; i < deliveryList.Count; i++)
                                {
                                    if (deliveryList[i].getTimeDelivery() == dt)
                                    {
                                        deliveryList[i].print();
                                    }
                                }
                            }
                            break;
                        case 3:
                            return;
                        default:
                            break;
                    }
                }
                catch (FormatException e)
                {
                    Console.WriteLine(e.Message);
                    return;
                }
                catch (FileNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                    return;
                }
                catch (Exception e) 
                {
                    Console.WriteLine(e.Message);
                    return;
                }
            } while (true);
        }
    }
}