using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MExcel = Microsoft.Office.Interop.Excel;

/**
Ali ARSLAN
Computer Engineer
aliarslan10@yandex.com.tr
**/

namespace FirmaBasariTahmini
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            agiKur();
            btnEgit.Enabled = false;
            radioButton1.Checked = true;
        }

             /* Daha kolay normalizasyon alabilmek için, aşağıdaki dizilerin içerisinde türdeş veriler eklendi. Yani eğitim verilerinin direkt kendisi değil.
             * Düşey olan veriler, ağın giriş verilerini oluşturmaktadır. Örneğin, her dizinin 1. indisinde yer alan değerlerin birleşimi bizim 1. eğitim 
             * veri setimizi oluşturmaktadır. Her birinin 2. indisinin değerlerinin birleşimi bizim "2. eğitim veri setimizi" oluşturuyor... vs.
             * Yani, ağa başka eğitim setleri girilecekse eğer, giriş verileri aşağıdaki kısma yatay olarak değil, dikey olarak eklenmeli. Örneğin,
             * 1. veri seti için 1. giriş verisi, V1'in  1.indisine
             * 1. veri seti için 2. giriş verisi, V2'nin 1. indisine
             * 1. veri seti için 2. giriş verisi, V3'ün 1. indisine ... eklenecek vs.
             */

        double[][] girdiler = new double[][] {
           new double[] { 0.0067, 0.1424, 0.0056, 0.2866, 0.3469, 0.0223, 0.0135, 0.0490, 0.0037, 0.0021, 0.0006, 0.0002, 0.000, 0.0003, 0.0002, 0.0002, 0.0009, 0.0548 },
           new double[] { 1.0745, 0.8690, 1.0380, 1.4327, 0.9541, 1.0204, 1.2254, 0.5884, 0.1921, 0.7324, 4.9130, 1.5749, 2.3653, 2.9510, 2.4606, 2.6820, 1.0321, 2.2080 },
           new double[] { 2.6148, 0.4483, 1.3873, 0.4854, 0.6129, 1.4134, 0.8506, 0.3226, 1.8057, 1.6619, 1.6244, 0.4896, 1.2666, 1.1269, 1.0025, 1.1307, 1.1927, 1.1361 },
           new double[] { 0.8686, 0.4879, 0.8639, 0.6827, 0.5965, 0.5871, 0.9086, 0.4508, 0.2227, 0.5772, 0.8522, 0.5964, 0.5316, 0.6601, 0.6120, 0.6159, 0.9347, 0.5926 },
           new double[] { 0.4378, 0.8571, 1.0587, 0.1958, 0.9193, 0.5803, 0.5663, 0.0529, 0.3388, 0.2058, 0.6057, 0.3259, 0.3022, 0.4113, 0.3627, 0.3718, 0.3705, 0.1560 },
           new double[] { 0.1734, 0.3930, 0.2525, 0.0485, 0.4134, 0.6227, 0.0525, 0.0226, 3.4007, 0.2615, 0.1413, 0.1003, 0.6874, 0.3097, 0.3518, 0.4146, 0.381, 0.1264 },
           new double[] { 0.0666, 0.0754, 0.5429, 0.1844, 0.1989, 0.3805, 0.4101, 0.2194, 1.2053, 0.9035, 0.8311, 0.0412, 0.3806, 0.4176, 0.3143, 0.3825, 0.8443, 0.3015 },
           new double[] { 20.3422, 1.6699, 6.7917, 10.4027, 2.2094, 2.8124, 15.7632, 37.9427, 5.0711, 11.0281, 8.1152, 4.2066, 1.8544, 4.7254, 3.8779, 3.7958, 39.3254, 5.2600 },
           new double[] { -0.0825, -0.0848, -0.0237, 0.0897, -0.0089, 0.0085, 0.0225, -0.0541, -0.3268, -0.0622, 0.0712, 0.1142, 0.0848, 0.0900, 0.0947, 0.0899, -0.0328, 0.0889 },
           new double[] { 0.4068, 1.6065, 0.6918, 1.7066, 1.1441, 0.5323, 1.2467, 1.6435, 0.2430, 0.4790, 0.8043, 1.4349, 0.4620, 0.9004, 0.9244, 0.7968, 0.9913, 0.7074 },
           new double[] { 1.0472, 0.6992, 0.6450, 0.8902, 0.6466, 0.7492, 0.6185, 0.1971, 0.0744, 0.2965, 3.2444, 1.4365, 1.7084, 2.1298, 1.8511, 1.9547, 0.3012, 1.6101 },
           new double[] { -0.1571, -0.1102, 0.0698, 0.1419, -0.0176, 0.0140, 0.1318, -0.2019, -13.1324, -0.5527, 0.3574, 0.1553, 0.5662, 0.3596, 0.3602, 0.4114, 0.0491, 0.5906 },
           new double[] { 1.1624, 0.7313, 0.8568, 0.6323, 0.7508, 0.6566, 0.7994, 0.9899, 1.3310, 0.9553, 0.6311, 0.4977, 0.3490, 0.4926, 0.4580, 0.4481, 0.9875, 0.3323 },
           new double[] { 0.4903, -0.4300, -0.2502, 0.2381, -0.0365, 0.0246, 0.1105, 11.7899, 1.0774, -1.7082, 0.1866, 0.2222, 0.1246, 0.1778, 0.1756, 0.1640, -5.4653, 0.1316 }
         }; // iki boyutlu dizi. sadece eğitim verileri yazıldı.

        double[] gercekCiktilar = new double[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1 };

        double[][]   noronlar = new double[4][];
        double[][][] agirliklar = new double[3][][]; // katman sayısı = 3 (0 dahil)
        double[][][] agirlikDegisimi = new double[3][][];
        double[][] hatalar = new double[3][];

        double[] ortalama, standartSapma;

        double[] esik = new double[] { 1, 1, 1 }; //default:1
        double[][] esikAgirligi = new double[3][];   // 3 tane eşik nöronu var
        double[][] esikAgirlikDegisimi = new double[3][];

        double ogrenmeKatsayisi = 0.9;
        double momentum = 0.8; //önerilen: 0.6 ile 0.8 arası.
        
        int araKatman1NoronSayisi, araKatman2NoronSayisi;

        Random uret = new Random(); //her defasında farklı değerler üretmesi için public olması gerekiyor.
        private double rastgeleAgirlikUret()
        {
            return (double)uret.Next(1, 999) / 1000.0;
        }

        int iterasyonSayisi;
        public void agiKur()
        {
            iterasyonSayisi = Convert.ToInt32(tbIterasyonSayisi.Text);

            araKatman1NoronSayisi = Convert.ToInt32(tbAraKatman1NoronSayisi.Text);
            araKatman2NoronSayisi = Convert.ToInt32(tbAraKatman2NoronSayisi.Text);

            // birinci boyutta 4 elemanlık bir dizi oluşturduk. Herbir indisinin içinde de yeni diziler oluşturduk.
            agirliklar[0] = new double[girdiler.Length][];
            agirliklar[1] = new double[araKatman1NoronSayisi][];
            agirliklar[2] = new double[araKatman2NoronSayisi][];

            agirlikDegisimi[0] = new double[girdiler.Length][];
            agirlikDegisimi[1] = new double[araKatman1NoronSayisi][];
            agirlikDegisimi[2] = new double[araKatman2NoronSayisi][];


            for (int i = 0; i < girdiler.Length; i++)
            {
                agirliklar[0][i] = new double[araKatman1NoronSayisi]; //giriş katmanından herbir ara katman-1 noronuna giden ağırlıklar
                agirlikDegisimi[0][i] = new double[araKatman1NoronSayisi];
            }


            for (int i = 0; i < araKatman1NoronSayisi; i++)
            {
                agirliklar[1][i] = new double[araKatman2NoronSayisi]; // ara katman-1 katmanından herbir ara katman-2 noronuna giden ağırlıklar
                agirlikDegisimi[1][i] = new double[araKatman2NoronSayisi];
            }

            for (int i = 0; i < araKatman2NoronSayisi; i++)
            {
                agirliklar[2][i] = new double[1]; // sıfır çünkü 1 tane çıkış nöronu var. O da 0 indisli.
                agirlikDegisimi[2][i] = new double[1];
            }

            /* 3 boyutlu dizi için RAM'de gerektiği kadar alan ayrıldı. Şimdi bunların nesnesi oluşturulacak:
            for (int a = 0; a < agirliklar.Length; a++)
            {
                for (int b = 0; b < agirliklar[a].Length; b++)
                {
                    for (int c = 0; c < agirliklar[a][b].Length; c++)
                    {
                        agirliklar[a][b][c] = new double();
                        agirlikDegisimi[a][b][c] = new double();
                    }
                }
            } */
            

            //Ağırlıklara başlangıç değerleri atanacak.
            for (int a = 0; a < agirliklar.Length; a++)
            {
                for (int b = 0; b < agirliklar[a].Length; b++)
                {
                    for (int c = 0; c < agirliklar[a][b].Length; c++)
                    {
                        agirliklar[a][b][c] = rastgeleAgirlikUret();
                        agirlikDegisimi[a][b][c] = 0;
                    }
                }
            }

            // Eşik ağırlıklarına rastgele değer ata ve değişimin başlangıç değerini sıfır yap:
            esikAgirligi[0] = new double[araKatman1NoronSayisi];
            esikAgirligi[1] = new double[araKatman2NoronSayisi];
            esikAgirligi[2] = new double[1];

            esikAgirlikDegisimi[0] = new double[araKatman1NoronSayisi];
            esikAgirlikDegisimi[1] = new double[araKatman2NoronSayisi];
            esikAgirlikDegisimi[2] = new double[1]; // çıkış nöronu

            for (int i = 0; i < esikAgirligi.Length; i++)
            {
                for (int j = 0; j < esikAgirligi[i].Length; j++)
                {
                    esikAgirligi[i][j] = rastgeleAgirlikUret();
                    esikAgirlikDegisimi[i][j] = 0;
                }
            }


            // Nöronları iki boyutlu dizide tanımlama ve başlangıç değerlerini sıfıra eşitleme işlemleri
            noronlar[0] = new double[girdiler.Length];          //1. katmandaki nöron sayısı
            noronlar[1] = new double[araKatman1NoronSayisi];    //2. katmandaki nöron sayısı
            noronlar[2] = new double[araKatman2NoronSayisi];    //3. katmandaki nöron sayısı
            noronlar[3] = new double[1];                        //4. katmandaki nöron sayısı (çıkış katmanı old. için : 1)

            /*
            for (int i = 0; i < noronlar.Length; i++)
            {
                for (int j = 0; j < noronlar[i].Length; j++)
			    {
			        noronlar[i][j] = 0;
			    }
            }*/

            // Ara nöronlar ve çıkış nöronuna ait hataların tutulduğu iki boyutlu dizi tanımlama işlemleri
            hatalar[0] = new double[araKatman1NoronSayisi];
            hatalar[1] = new double[araKatman2NoronSayisi];
            hatalar[2] = new double[1];


            labelSifirla();
            tumButonlarPasif();
            btnAgKur.Enabled = true;
            btnEgit.Enabled = true;
        }


        private double aktivasyon(double x) //sigmoid fonksiyonu
        {
            return (1 / (1 + Math.Exp(-x)));
        }

        private double ileriHesaplamaIslemleri(double[] girisler)
        {
            for (int i = 0; i < girisler.Length; i++)
            {
                noronlar[0][i] = girisler[i];
            }

            double toplam;
            for (int i = 0; i < 3; i++) //katman değiştirecek.
            {
                for (int j = 0; j < noronlar[i+1].Length; j++) //bir sonraki katmandaki nöron sayısı kadar dönder. (2)
                {
                    toplam = 0; //(14)
                    for (int k = 0; k < agirliklar[i].Length; k++) //mevcut katmandaki, mevcut nöronları tek tek gezip, ağırlıklarıyla çarpacak.
                    {
                        toplam = toplam +  (noronlar[i][k] * agirliklar[i][k][j]);
                    }

                    noronlar[i + 1][j] = toplam + (esik[i] * esikAgirligi[i][j]);
                    noronlar[i + 1][j] = aktivasyon(noronlar[i + 1][j]);
                }
            }

            /* Özetle:
            noronlar[1][0] = noronlar[0][0] * agirliklar[0][0][0] + //ara katman-1'in birinci nöron değerini hesaplamak için
                             noronlar[0][1] * agirliklar[0][1][0] + 
                             noronlar[0][2] * agirliklar[0][2][0] + 
                             noronlar[0][3] * agirliklar[0][3][0] +

            noronlar[1][0] = noronlar[0][0] * agirliklar[0][0][1] + //ara katman-1'in ikinci nöron değerini hesaplamak için
                             noronlar[0][1] * agirliklar[0][1][1] + 
                             noronlar[0][2] * agirliklar[0][2][1] + 
                             noronlar[0][3] * agirliklar[0][3][1] +
             ... vs.
             */

            return noronlar[3][0]; //çıkış nöronu
        }


        double cikis;
        ListBox hata = new ListBox();
        private void geriHesaplamaIslemleri(double[] girisler, double istenenCikis) //backpropagation ile eğitim
        {
            cikis = ileriHesaplamaIslemleri(girisler);
            if (radioButton1.Checked == true) { Application.DoEvents(); }
            

            // çıkış nöronundaki hata hesabı:
            hatalar[2][0] = cikis * (1 - cikis) * (istenenCikis - cikis); // çıkış norunu noronlar[3][0]'dir ve sabittir.

            // ara katman-2'deki hata hesabı:
            for (int i = 0; i < araKatman2NoronSayisi; i++)
            {
                hatalar[1][i] = noronlar[2][i] * (1 - noronlar[2][i]) * (agirliklar[2][i][0] * hatalar[2][0]); //hatalar[0] = arakatman-1'e denk geliyor fakat; noronlar[0], agirliklar[0] ve agirlikDegisimler[0] => giriş katmanına (1.katmana) denk geliyor.
            }

            // ara katman-1'deki hata hesabı:
            // önce hatalar toplamını al: (3. katmandaki hataların toplamı)
            double toplam;
                for (int j = 0; j < agirliklar[1].Length; j++) // noron değiştiriyor.
                {
                    toplam = 0;
                    for (int k = 0; k < agirliklar[1][j].Length; k++) //herbir nöronun tuttuğu ağırlıklar.
                    {
                       toplam = toplam + (agirliklar[1][j][k] * hatalar[1][k]);
                    }

                    double ortalamaHata = (toplam / hatalar[1].Length);
                    hatalar[0][j] = noronlar[1][j] * (1 - noronlar[1][j]) * ortalamaHata;
                 }

                /* Özetle:
                 * hatalar[0][0] = noronlar[1][0] * (1 - noronlar[1][0]) * ((agirliklar[1][0][0] * hatalar[1][0]) + (agirliklar[1][0][1] * hatalar[1][1]))
                 * hatalar[0][1] = noronlar[1][1] * (1 - noronlar[1][1]) * ((agirliklar[1][1][0] * hatalar[1][0]) + (agirliklar[1][1][1] * hatalar[1][1]))  
                 * .. vs. 
                 */



            // Ağırlıkların güncellenmesi : 
            for (int i = 0; i < noronlar.Length-1; i++) // katmanı değiştiriyor
            {
                for (int j = 0; j < noronlar[i].Length; j++) // nöronları  değiştiriyor
                {
                    for (int k = 0; k < noronlar[i+1].Length; k++) // ağırlıkları değiştiriyor
                    {
                        agirlikDegisimi[i][j][k] = ogrenmeKatsayisi * hatalar[i][k] * noronlar[i][j] + (momentum * agirlikDegisimi[i][j][k]);
                        agirliklar[i][j][k] = agirliklar[i][j][k] + agirlikDegisimi[i][j][k];
                    }
                }
            }


            /* Özetle:
             * agirlikDegisimi[0][0][0] = ogrKat * hata[0][0] * noronlar[0][0] + (momentum * agirlikDegisimi[0][0][0]);
             * agirlikDegisimi[0][0][1] = ogrKat * hata[0][1] * noronlar[0][0] + (momentum * agirlikDegisimi[0][0][1]);
             * ...vs. (1. katmanın 1. nöronuna ait ağırlıklar güncellendi.)
             * agirlikDegisimi[0][1][0] = ogrKat * hata[0][0] * noronlar[0][1] + (momentum * agirlikDegisimi[0][1][0]);
             * agirlikDegisimi[0][1][1] = ogrKat * hata[0][1] * noronlar[0][1] + (momentum * agirlikDegisimi[0][1][1]);
             * ...vs. (1. katmanın; 2. nöronuna ait ağırlıklar güncellendi.)
             */
             

            // eşik ağırlıklarının değişimi
            for (int i = 0; i < noronlar.Length-1; i++)
            {
                for (int j = 0; j < noronlar[i+1].Length; j++)
                {
                    esikAgirlikDegisimi[i][j] = ogrenmeKatsayisi * hatalar[i][j] * esik[i] + (momentum * esikAgirlikDegisimi[i][j]);
                    esikAgirligi[i][j] = esikAgirligi[i][j] + esikAgirlikDegisimi[i][j];
                }
            }

            double hataa = Math.Abs(hatalar[2][0]);
            hata.Items.Add(hataa);
        }

        public double ortalamaAl(double[] dizi)
        {
            double toplam = 0, sonuc;
            int boyut = dizi.Length;

            for (int i = 0; i < boyut; i++)
            {
                toplam = toplam + dizi[i];
            }

            sonuc = toplam / boyut;
            return sonuc;
        }

        public double standartSapmaHesapla(double[] dizi, double ortalama)
        {
            double toplam = 0, sonuc, fark;
            int boyut = dizi.Length;

            for (int i = 0; i < boyut; i++)
            {
                fark = dizi[i] - ortalama;
                toplam = toplam + Math.Pow(fark, 2);
            }

            sonuc = Math.Sqrt(toplam / (boyut-1));

            return sonuc;
        }


        double[][] testVerileri;
        public void btnEgit_Click(object sender, EventArgs e)
        {
            esik[0] = Convert.ToDouble(tbEsik1.Text);
            esik[1] = Convert.ToDouble(tbEsik2.Text);
            esik[2] = Convert.ToDouble(tbEsik3.Text);

            tumButonlarPasif();

            ortalama = new double[girdiler.Length];
            //türdeş verilerin ortalamasını al:
            for (int i = 0; i < ortalama.Length; i++)
            {
                ortalama[i] = ortalamaAl(girdiler[i]);
            }

         
            standartSapma = new double[girdiler.Length];
            //türdeş verilerin standart sapmasını al:
            for (int i = 0; i < ortalama.Length; i++)
            {
                standartSapma[i] = standartSapmaHesapla(girdiler[i], ortalama[i]);
            }

            // standart normalizasyon:
            for (int i = 0; i < girdiler.Length; i++)
            {
                for (int j = 0; j < girdiler[i].Length; j++)
                {
                    girdiler[i][j] = (girdiler[i][j] - ortalama[i]) / standartSapma[i];
                }
            }

            
            testVerileri = new double[4][];
            testVerileri[0] = new double[girdiler.Length];
            testVerileri[1] = new double[girdiler.Length];
            testVerileri[2] = new double[girdiler.Length];
            testVerileri[3] = new double[girdiler.Length];
            int indis_test;


            double[] girisVerileri = new double[girdiler[0].Length - testVerileri.Length]; // 4tanesi test verisi. içinin [0] olması önemli değil. Önemli olan girdi sayısının 4 eksiği olan indis sayısı.
            double[] girisVerileriYazdir = new double[girdiler[0].Length - testVerileri.Length];

            // geri yayılım ile eğit
            for (int i = 0; i < iterasyonSayisi; i++)
            {
                indis_test = 0;
                for (int sec = 0; sec < girdiler[0].Length; sec++)
                {    
                    if (sec == 0 || sec == 7 || sec == 12 || sec == 14) // 1,8,13,15 verileri test verisi olarak seçildi.
                    {
                        for (int k = 0; k < girdiler.Length; k++)
                        {
                            testVerileri[indis_test][k] = girdiler[k][sec];
                        }

                       indis_test++;
                     }

                    else // eğitilecek veriler:
                    {
                        for (int j = 0; j < girdiler.Length; j++)
                        {
                            girisVerileri[j] = girdiler[j][sec];
                            girisVerileriYazdir[j] = girdiler2[j][sec];
                        }

                        geriHesaplamaIslemleri(girisVerileri, gercekCiktilar[sec]);
                        girisVerisiniNoronaYazdir(girisVerileriYazdir);
                    }
                 }
              }

            tumButonlarAktif();
            btnEgit.Enabled = false;
            MessageBox.Show("Ağın eğitimi tamamlandı.\nAğı test edebilirsiniz.","Eğitim Tamamlandı",MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        
        public void girisVerisiniNoronaYazdir(double[] girdi)
        {
            // ---------------------------------------------------------------------------------------------------------------------
            // .............:::::::::::::::: NÖRON DEĞERLERİNİ LABEL'A YAZDIR ::::::::::::.........................
            // giriş katmanı nöronlarını yazdır:
            label1.Text = Math.Round(girdi[0],4).ToString(); label2.Text = Math.Round(girdi[1],4).ToString(); label3.Text = Math.Round(girdi[2],4).ToString(); 
            label4.Text = Math.Round(girdi[3],4).ToString(); label5.Text = Math.Round(girdi[4],4).ToString(); label6.Text = Math.Round(girdi[5],4).ToString();
            label7.Text = Math.Round(girdi[6],4).ToString(); label8.Text = Math.Round(girdi[7],4).ToString(); label9.Text = Math.Round(girdi[8],4).ToString();
            label10.Text = Math.Round(girdi[8],4).ToString(); label11.Text = Math.Round(girdi[10], 4).ToString(); label12.Text = Math.Round(girdi[11], 4).ToString();       
            label13.Text = Math.Round(girdi[12],4).ToString(); label14.Text = Math.Round(girdi[13],4).ToString();


            label15.Text = Math.Round(noronlar[1][0],6).ToString(); label16.Text = Math.Round(noronlar[1][1],6).ToString();
            label17.Text = Math.Round(noronlar[2][0], 6).ToString(); label18.Text = Math.Round(noronlar[2][1], 6).ToString(); 
            label19.Text = Math.Round(noronlar[3][0],6).ToString();
        }


        private void btnTest_Click(object sender, EventArgs e)
        {
            try
            {
                if(textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "" && textBox6.Text != "" &&
                    textBox7.Text != "" && textBox8.Text != "" && textBox9.Text != "" && textBox10.Text != "" && textBox11.Text != "" && textBox12.Text != "" &&
                    textBox13.Text != "" && textBox14.Text != "")
                {

                    noronlar[0][0] = double.Parse(textBox1.Text); noronlar[0][1] = float.Parse(textBox2.Text);
                    noronlar[0][2] = float.Parse(textBox3.Text);  noronlar[0][3] = float.Parse(textBox4.Text);
                    noronlar[0][4] = float.Parse(textBox5.Text); noronlar[0][5] = float.Parse(textBox6.Text);
                    noronlar[0][6] = float.Parse(textBox7.Text); noronlar[0][7] = float.Parse(textBox8.Text);
                    noronlar[0][8] = float.Parse(textBox9.Text); noronlar[0][9] = float.Parse(textBox10.Text);
                    noronlar[0][10] = float.Parse(textBox11.Text); noronlar[0][11] = float.Parse(textBox12.Text);
                    noronlar[0][12] = float.Parse(textBox13.Text); noronlar[0][13] = float.Parse(textBox14.Text);

                girisVerisiniNoronaYazdir(noronlar[0]);

                // standart normalizasyon (girilen verileri normalize ettikten sonra ağa ver)
                for (int i = 0; i < noronlar[0].Length; i++)
                {
                    noronlar[0][i] = (noronlar[0][i] - ortalama[i]) / standartSapma[i];
                }
                
 

                label19.Text = ileriHesaplamaIslemleri(noronlar[0]).ToString();
                }

                else
                {
                    MessageBox.Show("Eksik veri var. Lütfen tüm verileri giriniz.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            catch
            {
                MessageBox.Show("Boş giriş (kutucuk) bırakmayın", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }

        // Math.round(x,6) => virgülden sonra 6 basamak al.
        private void btnTest1_Click(object sender, EventArgs e)
        {
            label19.Text = Math.Round(ileriHesaplamaIslemleri(testVerileri[0]), 6).ToString(); //1 nolu veri seti. (dizinin sıfırdan başlamasından dolayı)
            goster(0); //1. eğitim seti test verisiydi çünkü.
            labelTest1_sonuc.Text = label19.Text;
        }

        private void btnTest2_Click(object sender, EventArgs e)
        {
            label19.Text = Math.Round(ileriHesaplamaIslemleri(testVerileri[1]), 6).ToString(); //1 nolu veri seti. (dizinin sıfırdan başlamasından dolayı)
            goster(7);
            labelTest2_sonuc.Text = label19.Text;
        }


        private void btnTest3_Click(object sender, EventArgs e)
        {
            label19.Text = Math.Round(ileriHesaplamaIslemleri(testVerileri[2]), 6).ToString(); //1 nolu veri seti. (dizinin sıfırdan başlamasından dolayı)
            goster(12);
            labelTest3_sonuc.Text = label19.Text;
        }

        private void btnTest4_Click(object sender, EventArgs e)
        {
            label19.Text = Math.Round(ileriHesaplamaIslemleri(testVerileri[3]), 6).ToString(); //1 nolu veri seti. (dizinin sıfırdan başlamasından dolayı)
            goster(14);
            labelTest4_sonuc.Text = label19.Text;
        }



        private void btnAgKur_Click(object sender, EventArgs e)
        {
            agiKur();
            btnAgKur.Enabled = false;
            this.Invalidate();
        }

        private void textKontrol_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar >= 47 && (int)e.KeyChar <= 57) // sadece rakamları yazdır.
            {

                e.Handled = false;

            }

            else if ((int)e.KeyChar == 8 || e.KeyChar == ',') // basılan tuş backspace ise yazdır
            {

                e.Handled = false;

            }

            else //bunların dışındaysa hiçbirisini yazdırma
            {

                e.Handled = true;
                MessageBox.Show("Sadece sayısal ifadeler girilebilir.\nOndalık sayılar için nokta değil, virgül kullanın.","Bilgi",MessageBoxButtons.OK, MessageBoxIcon.Information);
            } 
        }

        public void tumButonlarPasif()
        {
            btnAgKur.Enabled = true;
            btnEgit.Enabled = false;
            btnTest.Enabled = false;
            btnTest1.Enabled = false;
            btnTest2.Enabled = false;
            btnTest3.Enabled = false;
            btnTest4.Enabled = false;
            btnExcelHata.Enabled = false;
            btnSonucExcel.Enabled = false;
            btnVeriNormalizeExcel.Enabled = false;
        }

        public void tumButonlarAktif()
        {
            btnAgKur.Enabled = true;
            btnEgit.Enabled = true;
            btnTest.Enabled = true;
            btnTest1.Enabled = true;
            btnTest2.Enabled = true;
            btnTest3.Enabled = true;
            btnTest4.Enabled = true;
            btnExcelHata.Enabled = true;
            btnSonucExcel.Enabled = true;
            btnVeriNormalizeExcel.Enabled = true;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics grafik = this.CreateGraphics();
            Pen kalem1 = new Pen(Color.Blue, 2); //şekli çizmek için gerekli.
            Pen kalem2 = new Pen(Color.Gray, 2);
            Pen kalem3 = new Pen(Color.Black, 1);
            Brush firca = new SolidBrush(Color.Black); //string yazmak için mutlaka gerekiyor. parametre olarak.

            //Ellipse çizimleri - giriş katmanı nöronları ve içerikleri
            grafik.DrawEllipse(kalem1, 50, 0, 30, 30);
            grafik.DrawString("N1", new Font("Arial", 10, FontStyle.Regular), firca, 55, 8);

            grafik.DrawEllipse(kalem1, 50, 50, 30, 30);
            grafik.DrawString("N2", new Font("Arial", 10, FontStyle.Regular), firca, 55, 58);

            grafik.DrawEllipse(kalem1, 50, 100, 30, 30);
            grafik.DrawString("N3", new Font("Arial", 10, FontStyle.Regular), firca, 55, 108);

            grafik.DrawEllipse(kalem1, 50, 150, 30, 30);
            grafik.DrawString("N4", new Font("Arial", 10, FontStyle.Regular), firca, 55, 158);

            grafik.DrawEllipse(kalem1, 50, 200, 30, 30);
            grafik.DrawString("N5", new Font("Arial", 10, FontStyle.Regular), firca, 55, 208);

            grafik.DrawEllipse(kalem1, 50, 250, 30, 30);
            grafik.DrawString("N6", new Font("Arial", 10, FontStyle.Regular), firca, 55, 258);

            grafik.DrawEllipse(kalem1, 50, 300, 30, 30);
            grafik.DrawString("N7", new Font("Arial", 10, FontStyle.Regular), firca, 55, 308);

            grafik.DrawEllipse(kalem1, 50, 350, 30, 30);
            grafik.DrawString("N8", new Font("Arial", 10, FontStyle.Regular), firca, 55, 358);

            grafik.DrawEllipse(kalem1, 50, 400, 30, 30);
            grafik.DrawString("N9", new Font("Arial", 10, FontStyle.Regular), firca, 55, 408);

            grafik.DrawEllipse(kalem1, 50, 450, 30, 30);
            grafik.DrawString("N10", new Font("Arial", 10, FontStyle.Regular), firca, 52, 458);

            grafik.DrawEllipse(kalem1, 50, 500, 30, 30);
            grafik.DrawString("N11", new Font("Arial", 10, FontStyle.Regular), firca, 52, 508);

            grafik.DrawEllipse(kalem1, 50, 550, 30, 30);
            grafik.DrawString("N12", new Font("Arial", 10, FontStyle.Regular), firca, 52, 558);

            grafik.DrawEllipse(kalem1, 50, 600, 30, 30);
            grafik.DrawString("N13", new Font("Arial", 10, FontStyle.Regular), firca, 52, 608);

            grafik.DrawEllipse(kalem1, 50, 650, 30, 30);
            grafik.DrawString("N14", new Font("Arial", 10, FontStyle.Regular), firca, 52, 658);

            if (girdiler.Length > 14)
            {
                int ekNoron = girdiler.Length - 14;
                grafik.DrawEllipse(kalem1, 120, 650, 30, 30);
                grafik.DrawString("N+" + ekNoron.ToString(), new Font("Arial", 10, FontStyle.Regular), firca, 122, 658);
            }

            // Ellipse çizimleri -  Gizli katman 1
            grafik.DrawEllipse(kalem1, 400, 250, 30, 30);
            grafik.DrawString("N15", new Font("Arial", 10, FontStyle.Regular), firca, 402, 258);

            grafik.DrawEllipse(kalem1, 400, 370, 30, 30);
            grafik.DrawString("N16", new Font("Arial", 10, FontStyle.Regular), firca, 402, 378);

            if(Convert.ToInt32(tbAraKatman1NoronSayisi.Text) > 2)
            {
                int ekNoron = Convert.ToInt32(tbAraKatman1NoronSayisi.Text) - 2;
                grafik.DrawEllipse(kalem1, 400, 490, 30, 30);
                grafik.DrawString("N+" + ekNoron.ToString(), new Font("Arial", 10, FontStyle.Regular), firca, 402, 498);
            }

            // Ellipse çizimleri -  Gizli katman 2
            grafik.DrawEllipse(kalem1, 600, 250, 30, 30);
            grafik.DrawString("N17", new Font("Arial", 10, FontStyle.Regular), firca, 602, 258);

            grafik.DrawEllipse(kalem1, 600, 370, 30, 30);
            grafik.DrawString("N18", new Font("Arial", 10, FontStyle.Regular), firca, 602, 378);

            if (Convert.ToInt32(tbAraKatman2NoronSayisi.Text) > 2)
            {
                int ekNoron = Convert.ToInt32(tbAraKatman2NoronSayisi.Text) - 2;
                grafik.DrawEllipse(kalem1, 600, 490, 30, 30);
                grafik.DrawString("N+"+ekNoron.ToString(), new Font("Arial", 10, FontStyle.Regular), firca, 602, 498);
            }

            // çıkış katmanı
            grafik.DrawEllipse(kalem1, 800, 300, 30, 30);
            grafik.DrawString("N19", new Font("Arial", 10, FontStyle.Regular), firca, 802, 308);


            // Ellipse çizimleri -  Eşik değeri nöronları ve içerikleri
            grafik.DrawEllipse(kalem2, 200, 650, 30, 30);
            grafik.DrawString("E1", new Font("Arial", 10, FontStyle.Regular), firca, 205, 658);

            grafik.DrawEllipse(kalem2, 400, 650, 30, 30);
            grafik.DrawString("E2", new Font("Arial", 10, FontStyle.Regular), firca, 405, 658);

            grafik.DrawEllipse(kalem2, 600, 650, 30, 30);
            grafik.DrawString("E3", new Font("Arial", 10, FontStyle.Regular), firca, 605, 658);


            // nöronlar arasındaki ağırlık çizgilerinin çizimi:
            int deger = 20;
            for (int i = 0; i < 14; i++)
            {
                grafik.DrawLine(kalem3, 80, deger, 400, 265); // giriş nöronları ile nöron15 arası ağırlıkların çizimi
                grafik.DrawLine(kalem3, 80, deger, 400, 385);// giriş nöronları ile nöron16 arası ağırlıkların çizimi
                deger = deger + 50;
            }

            // ara katmanlar arası ağırlık çizgilerinin çizimi:
            grafik.DrawLine(kalem3, 430, 265, 600, 265);
            grafik.DrawLine(kalem3, 430, 265, 600, 385);
            grafik.DrawLine(kalem3, 430, 385, 600, 265);
            grafik.DrawLine(kalem3, 430, 385, 600, 385);

            // ara katman-2 ile çıkış katmanı arası ağırlık çizgilerinin çizimi
            grafik.DrawLine(kalem3, 630, 385, 800, 315);
            grafik.DrawLine(kalem3, 630, 265, 800, 315);

            // eşik değerleri ile nöronlar arası ağırlıkların çizimi
            grafik.DrawLine(kalem3, 220, 650, 400, 265); //eşik-1 için
            grafik.DrawLine(kalem3, 220, 650, 400, 385); //eşik-1 için

            grafik.DrawLine(kalem3, 415, 650, 600, 265); //eşik-2 için
            grafik.DrawLine(kalem3, 415, 650, 600, 385); //eşik-2 için

            grafik.DrawLine(kalem3, 615, 650, 800, 315); //eşik-3 için
        }

        private void btnVeriNormalizeExcel_Click(object sender, EventArgs e)
        {
            MExcel.Application excel_uygulamasi = new MExcel.Application();
            excel_uygulamasi.Visible = true;

            MExcel.Workbook excel_yazdir = excel_uygulamasi.Workbooks.Add(true);
            MExcel.Worksheet excel_sayfasi = (MExcel.Worksheet)excel_uygulamasi.Sheets[1];

            for (int i = 0; i < girdiler[0].Length; i++)
            {                                   
                try
                {
                    MExcel.Range v1Yazdir = (MExcel.Range)excel_sayfasi.Cells[1, i+1];
                    v1Yazdir.Value2 = Math.Round(girdiler[0][i],4);

                    MExcel.Range v2Yazdir = (MExcel.Range)excel_sayfasi.Cells[2, i+1];
                    v2Yazdir.Value2 = Math.Round(girdiler[1][i], 4);

                    MExcel.Range v3Yazdir = (MExcel.Range)excel_sayfasi.Cells[3, i+1];
                    v3Yazdir.Value2 = Math.Round(girdiler[2][i], 4);

                    MExcel.Range v4Yazdir = (MExcel.Range)excel_sayfasi.Cells[4, i+1];
                    v4Yazdir.Value2 = Math.Round(girdiler[3][i], 4);

                    MExcel.Range v5Yazdir = (MExcel.Range)excel_sayfasi.Cells[5, i+1];
                    v5Yazdir.Value2 = Math.Round(girdiler[4][i], 4);

                    MExcel.Range v6Yazdir = (MExcel.Range)excel_sayfasi.Cells[6, i+1];
                    v6Yazdir.Value2 = Math.Round(girdiler[5][i], 4);

                    MExcel.Range v7Yazdir = (MExcel.Range)excel_sayfasi.Cells[7, i+1];
                    v7Yazdir.Value2 = Math.Round(girdiler[6][i], 4);

                    MExcel.Range v8Yazdir = (MExcel.Range)excel_sayfasi.Cells[8, i+1];
                    v8Yazdir.Value2 = Math.Round(girdiler[7][i], 4);

                    MExcel.Range v9Yazdir = (MExcel.Range)excel_sayfasi.Cells[9, i+1];
                    v9Yazdir.Value2 = Math.Round(girdiler[8][i], 4);

                    MExcel.Range v10Yazdir = (MExcel.Range)excel_sayfasi.Cells[10, i+1];
                    v10Yazdir.Value2 = Math.Round(girdiler[9][i], 4);

                    MExcel.Range v11Yazdir = (MExcel.Range)excel_sayfasi.Cells[11, i+1];
                    v11Yazdir.Value2 = Math.Round(girdiler[10][i], 4);

                    MExcel.Range v12Yazdir = (MExcel.Range)excel_sayfasi.Cells[12, i+1];
                    v12Yazdir.Value2 = Math.Round(girdiler[11][i], 4);

                    MExcel.Range v13Yazdir = (MExcel.Range)excel_sayfasi.Cells[13, i+1];
                    v13Yazdir.Value2 = Math.Round(girdiler[12][i], 4);

                    MExcel.Range v14Yazdir = (MExcel.Range)excel_sayfasi.Cells[14, i+1];
                    v14Yazdir.Value2 = Math.Round(girdiler[13][i], 4);
                }

                catch
                {

                }

                finally
                {
                   // nothing to do
                }
            }
        }

        private void btnExcelHata_Click(object sender, EventArgs e)
        {
            MExcel.Application excel_uygulamasi = new MExcel.Application();
            excel_uygulamasi.Visible = true;

            MExcel.Workbook excel_yazdir = excel_uygulamasi.Workbooks.Add(true);
            MExcel.Worksheet excel_sayfasi = (MExcel.Worksheet)excel_uygulamasi.Sheets[1];

            for (int i = 0; i < iterasyonSayisi; i++) //v1'de özdeş veriler var. her bir v1'de 18 tane veri var. excel'de her bir satır yatay olarak 
            {                                   // ağın bir giriş veri setini oluşturmuş olacak.
                try
                {
                    MExcel.Range hataYazdir = (MExcel.Range)excel_sayfasi.Cells[i+1, 1];
                    hataYazdir.Value2 = hata.Items[i];
                }

                catch
                {

                }

                finally
                {
                    // nothing to do
                }
            }
        }


        private void btnSonucExcel_Click(object sender, EventArgs e)
        {
            double[] veriSetiCiktisi = new double[girdiler[0].Length]; //18 tane
            double[] giris = new double[girdiler.Length]; // 14 tane

            for (int i = 0; i < girdiler[0].Length; i++)
            {
                for (int j = 0; j < girdiler.Length; j++)
                {
                    giris[j] = girdiler[j][i];
                }

                veriSetiCiktisi[i] = ileriHesaplamaIslemleri(giris);
                girisVerisiniNoronaYazdir(giris);
            }


            MExcel.Application excel_uygulamasi = new MExcel.Application();
            excel_uygulamasi.Visible = true;

            MExcel.Workbook excel_yazdir = excel_uygulamasi.Workbooks.Add(true);
            MExcel.Worksheet excel_sayfasi = (MExcel.Worksheet)excel_uygulamasi.Sheets[1];

            for (int i = 0; i < veriSetiCiktisi.Length; i++)
            {
                MExcel.Range ciktiYazdir = (MExcel.Range)excel_sayfasi.Cells[i+1, 1];
                ciktiYazdir.Value2 = veriSetiCiktisi[i];
           }
        }

        public void labelSifirla()
        {
            label1.Text = "0.0000"; label2.Text = "0.0000"; label3.Text = "0.0000"; label4.Text = "0.0000";
            label5.Text = "0.0000"; label6.Text = "0.0000"; label7.Text = "0.0000"; label8.Text = "0.0000";
            label9.Text = "0.0000"; label10.Text = "0.0000"; label11.Text = "0.0000"; label12.Text = "0.0000";
            label13.Text = "0.0000"; label14.Text = "0.0000"; label15.Text = "0.0000"; label16.Text = "0.0000";
            label17.Text = "0.0000"; label18.Text = "0.0000"; label19.Text = "0.0000";
        }

        double[][] girdiler2 = new double[][] {
           new double[] { 0.0067, 0.1424, 0.0056, 0.2866, 0.3469, 0.0223, 0.0135, 0.0490, 0.0037, 0.0021, 0.0006, 0.0002, 0.000, 0.0003, 0.0002, 0.0002, 0.0009, 0.0548 },
           new double[] { 1.0745, 0.8690, 1.0380, 1.4327, 0.9541, 1.0204, 1.2254, 0.5884, 0.1921, 0.7324, 4.9130, 1.5749, 2.3653, 2.9510, 2.4606, 2.6820, 1.0321, 2.2080 },
           new double[] { 2.6148, 0.4483, 1.3873, 0.4854, 0.6129, 1.4134, 0.8506, 0.3226, 1.8057, 1.6619, 1.6244, 0.4896, 1.2666, 1.1269, 1.0025, 1.1307, 1.1927, 1.1361 },
           new double[] { 0.8686, 0.4879, 0.8639, 0.6827, 0.5965, 0.5871, 0.9086, 0.4508, 0.2227, 0.5772, 0.8522, 0.5964, 0.5316, 0.6601, 0.6120, 0.6159, 0.9347, 0.5926 },
           new double[] { 0.4378, 0.8571, 1.0587, 0.1958, 0.9193, 0.5803, 0.5663, 0.0529, 0.3388, 0.2058, 0.6057, 0.3259, 0.3022, 0.4113, 0.3627, 0.3718, 0.3705, 0.1560 },
           new double[] { 0.1734, 0.3930, 0.2525, 0.0485, 0.4134, 0.6227, 0.0525, 0.0226, 3.4007, 0.2615, 0.1413, 0.1003, 0.6874, 0.3097, 0.3518, 0.4146, 0.381, 0.1264 },
           new double[] { 0.0666, 0.0754, 0.5429, 0.1844, 0.1989, 0.3805, 0.4101, 0.2194, 1.2053, 0.9035, 0.8311, 0.0412, 0.3806, 0.4176, 0.3143, 0.3825, 0.8443, 0.3015 },
           new double[] { 20.3422, 1.6699, 6.7917, 10.4027, 2.2094, 2.8124, 15.7632, 37.9427, 5.0711, 11.0281, 8.1152, 4.2066, 1.8544, 4.7254, 3.8779, 3.7958, 39.3254, 5.2600 },
           new double[] { -0.0825, -0.0848, -0.0237, 0.0897, -0.0089, 0.0085, 0.0225, -0.0541, -0.3268, -0.0622, 0.0712, 0.1142, 0.0848, 0.0900, 0.0947, 0.0899, -0.0328, 0.0889 },
           new double[] { 0.4068, 1.6065, 0.6918, 1.7066, 1.1441, 0.5323, 1.2467, 1.6435, 0.2430, 0.4790, 0.8043, 1.4349, 0.4620, 0.9004, 0.9244, 0.7968, 0.9913, 0.7074 },
           new double[] { 1.0472, 0.6992, 0.6450, 0.8902, 0.6466, 0.7492, 0.6185, 0.1971, 0.0744, 0.2965, 3.2444, 1.4365, 1.7084, 2.1298, 1.8511, 1.9547, 0.3012, 1.6101 },
           new double[] { -0.1571, -0.1102, 0.0698, 0.1419, -0.0176, 0.0140, 0.1318, -0.2019, -13.1324, -0.5527, 0.3574, 0.1553, 0.5662, 0.3596, 0.3602, 0.4114, 0.0491, 0.5906 },
           new double[] { 1.1624, 0.7313, 0.8568, 0.6323, 0.7508, 0.6566, 0.7994, 0.9899, 1.3310, 0.9553, 0.6311, 0.4977, 0.3490, 0.4926, 0.4580, 0.4481, 0.9875, 0.3323 },
           new double[] { 0.4903, -0.4300, -0.2502, 0.2381, -0.0365, 0.0246, 0.1105, 11.7899, 1.0774, -1.7082, 0.1866, 0.2222, 0.1246, 0.1778, 0.1756, 0.1640, -5.4653, 0.1316 }
         }; // program arayüzünde normalizasyona uğramayan gerçek verileri gösterebilmek için tanımlandı.

        public void goster(int i)
        {
            label1.Text = girdiler2[0][i].ToString(); label2.Text = girdiler2[1][i].ToString(); label3.Text = girdiler2[2][i].ToString();
            label4.Text = girdiler2[3][i].ToString(); label5.Text = girdiler2[4][i].ToString(); label6.Text = girdiler2[5][i].ToString();
            label7.Text = girdiler2[6][i].ToString(); label8.Text = girdiler2[7][i].ToString(); label9.Text = girdiler2[8][i].ToString();
            label10.Text = girdiler2[9][i].ToString(); label11.Text = girdiler2[10][i].ToString(); label12.Text = girdiler2[11][i].ToString();
            label13.Text = girdiler2[12][i].ToString(); label14.Text = girdiler2[13][i].ToString();

            label15.Text = Math.Round(noronlar[1][0],6).ToString();
            label16.Text = Math.Round(noronlar[1][1],6).ToString();
            label17.Text = Math.Round(noronlar[2][0],6).ToString();
            label18.Text = Math.Round(noronlar[2][1], 6).ToString();
        }
    }
}
