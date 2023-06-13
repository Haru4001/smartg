using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace smartga
{
    public partial class Form2 : Form
    {
        private double Humidity; // 습도
        private double Temperature; // 온도
        private int Dustlevel; // 미세먼지 수치

        // 아두이노와의 시리얼 통신을 위한 SerialPort 객체 생성
        private SerialPort arduinoPort;
        private bool HumidifierOn = false;

        public Form2()
        {
            InitializeComponent();
            // 시리얼 통신을 위한 포트 이름과 속도 설정
            arduinoPort = new SerialPort("COM10", 9600);
            arduinoPort.Open(); // 시리얼 포트 열기
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double minhumidity = 40;
            double maxhumidity = 60;

            if (Humidity >= minhumidity && Humidity <= maxhumidity)
            {
                textBox1.Text = "적정 습도입니다.";
            }
            else 
            {
                textBox1.Text = "습도가 낮습니다. 위험!";
            }

            if (Dustlevel > 70)
            {
                textBox2.Text = "미세먼지 수치가 높습니다. 가습기 사용을 자제하세요!";
            }
            else
            {
                textBox2.Text = "미세먼지 수치에 문제가 없습니다. 가습기를 사용하셔도 좋습니다.";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            HumidifierOn =! HumidifierOn;

            if (HumidifierOn)
            {
                // 아두이노에 신호를 보내 가습기 센서를 켜는 동작 수행
                arduinoPort.Write("1");
            }
            else
            {
                // 아두이노에 신호를 보내 가습기 센서를 끄는 동작 수행
                arduinoPort.Write("0");
            }
        }
    }
}
        
    

