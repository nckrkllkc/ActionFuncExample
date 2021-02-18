using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace ActionFuncExample
{
    class Program
    {
        public delegate void EkleFunc();
        static void Main(string[] args)
        {
            //delegate metotlar için imza tutan bir veri tipidir. (metotların dönüş tipi ve parametreleri)
            //delegate'ler bir metodu başka bir metoda parametre olarak göndermek için kullanılabildiği gibi
            //bir metodu bir değişkene değer olarak atamak veya birden fazla metodu tek komutla çalıştırmak
            //için de kullanılır.

            //callback - event handler ve lambda expression yapılarında kullanılır.

            //callback -> bir metoda başka bir metodu parametre olarak gönderip yapılacak işler bitince başka bir metodun
            //çalışması sağlanır.

            //Sınıflarımızda generic yapıyı sağlamak için interface kullandığımız gibi, metotlarda da gönderilen metotlara
            //göre işlem yapmasını sağlamak için de delegate'lerden yardım alırız.
            Ekle(KisiEkle);
            Ekle(AracEkle);
            Ekle2();

            //Action parametreli
            Hesapla(Topla, 2, 1);

            //predicate
            KontrolEt(IsGreaterThanZero, 10);

            //func kullanımları
            Func<int, int, bool> funcOrnek = delegate (int sayi1, int sayi2) {
                if (sayi1 > sayi2)
                {
                    return true;
                }
                return false;
            };

            //ya da
            funcOrnek = null;
            funcOrnek = SayiKontrol;
            funcOrnek(1, 2);

            //ya da
            FuncMethod(SayiKontrol, 1, 2);
        }

        public static void Ekle2()
        {
            //parametre olarak direkt metot adı yazılabildiği gibi, oluşturulan delegate değişkeninin içerisine metot değer olarak atanıp
            //bu da parametre olarak verilebilir.
            EkleFunc fonksiyon = KisiEkle;
            fonksiyon += AracEkle; //metot ekleme
            fonksiyon -= AracEkle; //metot çıkarma
            Ekle(fonksiyon);

        }
        public static void Ekle(EkleFunc ekleFunc)
        {
            //parametre olarak gönderilen metodun referansıyla ilgili metot çalıştırılmıştır.
            ekleFunc();
            //ya da
            ekleFunc.Invoke();
        }

        public static void AracEkle()
        {
            Console.WriteLine("Araç Eklendi.");
        }

        public static void KisiEkle()
        {
            Console.WriteLine("Kişi Eklendi.");
        }

        public static void Topla(int sayi1, int sayi2)
        {
            Console.WriteLine(sayi1 + sayi2);
        }

        public static void Hesapla(Action<int,int> fonksiyon, int sayi1, int sayi2)
        {
            //fonksiyonun içi null değilse
            fonksiyon?.Invoke(sayi1,sayi2);
        }

        public static void KontrolEt(Predicate<int> kontrol,int sayi1)
        {
            kontrol?.Invoke(sayi1);
        }

        public static bool IsGreaterThanZero(int sayi1)
        {
            if (sayi1>0)
            {
                return true;
            }
            return false;
        }

        public static bool FuncMethod(Func<int,int,bool> kontrol, int sayi1, int sayi2)
        {
            return kontrol(sayi1,sayi2);
        }

        public static bool SayiKontrol(int sayi1, int sayi2)
        {
            if (sayi1> sayi2)
            {
                return true;
            }
            return false;
        }
    }
}
