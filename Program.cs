using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsoleApplication
{
    public class Delivery : IComparable
    {
        private double distance;
        private string obj_name, car_sign;
        private DateTime time_delivery;
        
        public double Distance
        {
            get { return distance; }
            set { distance = value; }
        }

        public string Obj_Name
        {
            get { return obj_name; }
            set { obj_name = value; }
        }

        public string Car_Sign
        {
            get { return car_sign; }
            set { car_sign = value; }
        }

        public DateTime Time_Delivery
        {
            get { return time_delivery; }
            set { time_delivery = value; }
        }

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

        int IComparable.CompareTo(object o)
        {
            Delivery temp = o as Delivery;
            if (this.distance < temp.distance)
            {
                return -1;
            }
            if (this.distance > temp.distance) 
            { 
                return 1;
            }
            return 0;
        }

        private class SortByObjNameH : IComparer<object>
        {
            int IComparer<object>.Compare(object o1, object o2)
            {
                Delivery obj1 = o1 as Delivery;
                Delivery obj2 = o2 as Delivery;
                return string.Compare(obj1.Obj_Name, obj2.Obj_Name);
            }
        }

        private class SortByCarSignNameH : IComparer<object>
        {
            int IComparer<object>.Compare(object o1, object o2)
            {
                Delivery obj1 = o1 as Delivery;
                Delivery obj2 = o2 as Delivery;
                return string.Compare(obj1.Car_Sign, obj2.Car_Sign);
            }
        }

        private class SortByDateTimeH : IComparer<object>
        {
            int IComparer<object>.Compare(object o1, object o2)
            {
                Delivery obj1 = o1 as Delivery;
                Delivery obj2 = o2 as Delivery;
                return DateTime.Compare(obj1.Time_Delivery, obj2.Time_Delivery);
            }
        }

        public static IComparer<object> SortByObjName
        {
            get { return (IComparer<object>) new SortByObjNameH(); }
        }
        public static IComparer<object> CarSign
        {
            get { return (IComparer<object>) new SortByCarSignNameH(); }
        }
        public static IComparer<object> SortByDateTime
        {
            get { return (IComparer<object>) new SortByDateTimeH(); }
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
            // Delivery[] deliveryList = new Delivery[10];
            do
            {
                try
                {
                    string usr_inp, s;
                    int i_usr_inp;
                    Console.WriteLine("\t\t\t 1. Input Information \n\t\t\t 2. Sort by user keyword \n\t\t\t 3. Array Sort by distance, car sign or time \n\t\t\t 4. Exit");
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

                                }
                            }
                            break;
                        case 2:
                            {
                                Console.WriteLine("\t\t\tInput your date (format: year,month,day)");
                                usr_inp = Console.ReadLine();
                                string[] datetime = usr_inp.Split(new char[] { ',', ' ' });
                                DateTime dt = new DateTime(int.Parse(datetime[0]), int.Parse(datetime[1]), int.Parse(datetime[2]));

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
                            {
                                deliveryList.Sort();
                                Console.WriteLine("\t\t\t Sorted by default");
                                foreach (Delivery delivery in deliveryList)
                                {
                                    delivery.print();
                                }
                                deliveryList.Sort(Delivery.SortByObjName);
                                Console.WriteLine("\t\t\t Sorted by object name");
                                foreach (Delivery delivery in deliveryList)
                                {
                                    delivery.print();
                                }
                                deliveryList.Sort(Delivery.SortByDateTime);
                                Console.WriteLine("\t\t\t Sorted by date and time");
                                foreach (Delivery delivery in deliveryList)
                                {
                                    delivery.print();
                                }
                                // Array.Sort(deliveryList, Delivery.SortByObjName);
                            }
                            break;
                        case 4:
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