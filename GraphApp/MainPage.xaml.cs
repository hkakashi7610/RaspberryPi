using LiveCharts;
using LiveCharts.Uwp;
using System;
using Windows.Devices.Gpio;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// 空白ページの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x411 を参照してください

namespace GraphApp
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private readonly int[] arrPIN = new int[]{5,6,13,19,26,16,20,21};
        private DispatcherTimer timer;
        private GpioPin[] arrGpioPin = new GpioPin[8];
        public SeriesCollection Sc { get; set; } = new SeriesCollection();

        public MainPage()
        {
            this.InitializeComponent();

            InitGPIO();
            InitGraph();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1000);
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
                GpioStatus.Text = "There is no GPIO controller on this device.";
                return;
            }

            for (int i = 0; i < arrPIN.Length; i++)
            {
                arrGpioPin[i] = gpio.OpenPin(arrPIN[i]);
                arrGpioPin[i].Write(GpioPinValue.Low);
                if (arrGpioPin[i].IsDriveModeSupported(GpioPinDriveMode.InputPullUp))
                {
                    arrGpioPin[i].SetDriveMode(GpioPinDriveMode.InputPullUp);
                }
                else
                {
                    arrGpioPin[i].SetDriveMode(GpioPinDriveMode.Input);
                }
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
                new LineSeries
                {
                    //系列値
                    Values = new ChartValues<int>(),
                    //線の色
                    Stroke = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Colors.Red),
                    //直線のスムージング（0：なし、省略：あり）
                    LineSmoothness = 0,
                    PointGeometry = DefaultGeometries.None,
                });
            Sc.Add(
                new LineSeries
                {
                    //系列値
                    Values = new ChartValues<int>(),
                    //線の色
                    Stroke = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Colors.Yellow),
                    //直線のスムージング（0：なし、省略：あり）
                    LineSmoothness = 0,
                    PointGeometry = DefaultGeometries.None,
                });
            Sc.Add(
                new LineSeries
                {
                    //系列値
                    Values = new ChartValues<int>(),
                    //線の色
                    Stroke = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Colors.Purple),
                    //直線のスムージング（0：なし、省略：あり）
                    LineSmoothness = 0,
                    PointGeometry = DefaultGeometries.None,
                });
            Sc.Add(
                new LineSeries
                {
                    //系列値
                    Values = new ChartValues<int>(),
                    //線の色
                    Stroke = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Colors.Green),
                    //直線のスムージング（0：なし、省略：あり）
                    LineSmoothness = 0,
                    PointGeometry = DefaultGeometries.None,
                });
            Sc.Add(
                new LineSeries
                {
                    //系列値
                    Values = new ChartValues<int>(),
                    //線の色
                    Stroke = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Colors.Orange),
                    //直線のスムージング（0：なし、省略：あり）
                    LineSmoothness = 0,
                    PointGeometry = DefaultGeometries.None,
                });
            Sc.Add(
                new LineSeries
                {
                    //系列値
                    Values = new ChartValues<int>(),
                    //線の色
                    Stroke = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Colors.Blue),
                    //直線のスムージング（0：なし、省略：あり）
                    LineSmoothness = 0,
                    PointGeometry = DefaultGeometries.None,
                });
            Sc.Add(
                new LineSeries
                {
                    //系列値
                    Values = new ChartValues<int>(),
                    //線の色
                    Stroke = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Colors.White),
                    //直線のスムージング（0：なし、省略：あり）
                    LineSmoothness = 0,
                    PointGeometry = DefaultGeometries.None,
                });
            Sc.Add(
                new LineSeries
                {
                    //系列値
                    Values = new ChartValues<int>(),
                    //線の色（省略：自動で配色されます）
                    Stroke = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Colors.Chartreuse),
                    //直線のスムージング（0：なし、省略：あり）
                    LineSmoothness = 0,
                    PointGeometry = DefaultGeometries.None,
                });

            /////////////////////////////////////
            //ステップ２:LiveChartの設定
            /////////////////////////////////////
            //凡例の表示位置
            //LC_Graph.LegendLocation = LegendLocation.Right;

            //軸の設定
            LC_Graph.AxisX.Clear();     //デフォルトで設定されている軸をクリア
            //LC_Graph.AxisX.Add(new Axis { Title = "横軸", FontSize = 20 });
            LC_Graph.AxisY.Clear();
            LC_Graph.AxisY.Add(new Axis { FontSize = 20, MinValue = 0 });
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

        private GpioPinValue pinValue;

        private void Timer_Tick(object sender, object e)
        {
            //各系列に、それぞれ値を代入
            for (int iSeries = 0; iSeries < Sc.Count; iSeries++)
            {
                GpioPinValue pinValue = arrGpioPin[iSeries].Read();
                int value = (pinValue == GpioPinValue.High) ? 1 : 0;
                
                if (Sc[iSeries].Values.Count > 20)
                {
                    Sc[iSeries].Values.RemoveAt(0);
                }
                
                Sc[iSeries].Values.Add(value + (iSeries * 3));   //上方向に積み上げるための3＋


            }
        }
    }
}
