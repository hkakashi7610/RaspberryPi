using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using LiveCharts;
using LiveCharts.Uwp;
using Windows.Devices.Gpio;

// 空白ページの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x411 を参照してください

namespace GraphApp
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private const int LED_PIN = 27;
        private DispatcherTimer timer;
        private GpioPin pin;
        public SeriesCollection Sc { get; set; } = new SeriesCollection();

        public MainPage()
        {
            this.InitializeComponent();

            InitGPIO();
            InitGraph();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Tick += Timer_Tick;
            timer.Start();

            DataContext = this;
        }

        private void InitGPIO()
        {
            var gpio = GpioController.GetDefault();

            // Show an error if there is no GPIO controller
            if (gpio == null)
            {
                pin = null;
                GpioStatus.Text = "There is no GPIO controller on this device.";
                return;
            }

            
            pin = gpio.OpenPin(LED_PIN);
            pin.Write(GpioPinValue.Low);
            if (pin.IsDriveModeSupported(GpioPinDriveMode.InputPullUp))
            {
                pin.SetDriveMode(GpioPinDriveMode.InputPullUp);
            }
            else
            {
                pin.SetDriveMode(GpioPinDriveMode.Input);
            }

            GpioStatus.Text = "GPIO pin initialized correctly.";

        }

        private void InitGraph()
        {
            /////////////////////////////////////
            //ステップ１：系列にグラフを追加
            /////////////////////////////////////
            Sc.Clear();
            Sc.Add(
                new LineSeries                     //折れ線グラフ
                {
                    //凡例名
                    Title = "折れ線",
                    //系列値
                    Values = new ChartValues<int>(),
                    //線の色（省略：自動で配色されます）
                    //Stroke = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Colors.Red),
                    //直線のスムージング（0：なし、省略：あり）
                    //LineSmoothness = 0,

                });

            //Sc.Add(
            //    new ColumnSeries                    //棒グラフ
            //    {
            //        Title = "棒",
            //        Values = new ChartValues<long>(),
            //        //Stroke = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Colors.Red),
            //    });

            //Sc.Add(
            //    new StepLineSeries                  //線グラフ
            //    {
            //        Title = "線",
            //        Values = new ChartValues<long>(),
            //        //Stroke = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Colors.Red),
            //    });


            /////////////////////////////////////
            //ステップ２:LiveChartの設定
            /////////////////////////////////////
            //凡例の表示位置
            LC_Graph.LegendLocation = LegendLocation.Right;

            //軸の設定
            LC_Graph.AxisX.Clear();     //デフォルトで設定されている軸をクリア
            LC_Graph.AxisX.Add(new Axis { Title = "横軸", FontSize = 20 });
            LC_Graph.AxisY.Clear();
            LC_Graph.AxisY.Add(new Axis { Title = "縦軸", FontSize = 20, MaxValue = 1, MinValue = 0 });
        }

        /// <summary>
        /// ボタンをクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Draw_Click(object sender, RoutedEventArgs e)
        {
            if (timer.IsEnabled)
            {
                timer.Stop();
            }
            else
            {
                timer.Start();
            }

        }

        private void Timer_Tick(object sender, object e)
        {
            Random rnd = new Random();

            //各系列に、それぞれ値を代入
            for (int iSeries = 0; iSeries < Sc.Count; iSeries++)
            {
                GpioPinValue pinValue =  pin.Read();
                int value = (pinValue == GpioPinValue.High) ? 1 : 0;
                Sc[iSeries].Values.Add(value);
            }
        }
    }
}
