# Raspberry Piを使ったツールの作成
## Raspberry Pi とは

- Raspberry Pi（ラズベリー パイ）は、ARMプロセッサを搭載したシングルボードコンピュータ
- イギリスのラズベリーパイ財団によって開発されている
- 日本語では略称として「ラズパイ」とも呼ばれる
- 教育で利用されることを想定して制作された
- IoTが隆盛した2010年代後半以降は、安価に入手できるシングルボードコンピュータとして趣味や業務（試作品の開発）等としても用いられるようになった
- オフィシャルサイト(https://www.raspberrypi.org/)
- Wikipedia(https://ja.wikipedia.org/wiki/Raspberry_Pi)
- Raspberry Pi 3 Model B (発売日：2016年2月29日)
- Raspberry Pi 3 Model B+(発売日：2018年3月14日)
- Raspberry Pi 4 Model B (発売日：2019年6月24日)
- 利用できるOSは、Raspberry Pi OS(旧称:Rasbpian)、Arch Linux、Fedora、Ubuntu MATE、Windows10IoT
- Windows 10 IoT Coreがサポートするのは、Raspberry Pi 2と3Bのｍに。3B+はサポート外
- https://docs.microsoft.com/en-us/windows/iot-core/tutorials/quickstarter/PrototypeBoards

### [例]Raspberry Pi 3 Model B Specification
- Quad Core 1.2GHz Broadcom BCM2837 64bit CPU
- 1GB RAM
- BCM43438 wireless LAN and Bluetooth Low Energy (BLE) on board
- 100 Base Ethernet
- 40-pin extended GPIO
- 4 USB 2 ports
- 4 Pole stereo output and composite video port
- Full size HDMI
- CSI camera port for connecting a Raspberry Pi camera
- DSI display port for connecting a Raspberry Pi touchscreen display
- Micro SD port for loading your operating system and storing data
- Upgraded switched Micro USB power source up to 2.5A

![Raspberry Pi Model B(View)](https://www.raspberrypi.org/homepage-9df4b/static/2efe82d07c653f3f638c0cbf54346b5d/ae23f/67d8fcc5b2796665a45f61a2e8a5bb7f10cdd3f5_raspberry-pi-3-1-1619x1080.jpg)
-
![Raspberry Pi Model B(Pin)](https://docs.microsoft.com/ja-jp/windows/iot-core/media/pinmappingsrpi/rp2_pinout.png)

### [例]Raspberry Pi 4 Model B

![Raspberry Pi Model B](https://www.raspberrypi.org/homepage-9df4b/static/raspberry-pi-4-labelled-2857741801afdf1cabeaa58325e07b58.png)
-

## OSに Windows 10 IoT Core を使う場合

### Windows 10 IoT Coreとは

参考サイト(https://docs.microsoft.com/ja-jp/windows/iot-core/windows-iot)

- Windows 10 IoT は、Windows 10 ファミリのメンバーで主に組み込みをターゲット
- Windows 10 IoT Core と Windows 10 IoT Enterprise の2つのエディション
- Windows 10 IoT Enterpriseは、ほぼPC版と同じで、x86(x64)のみサポート
- Windows 10 IoT Core は、UWP UIのみサポート(1度に1つのみ)し、ARMをサポートする。Raspberry Piは 2と3のみをサポート
- Windowsの次の長期サポート（LTS）リリースでは、Windows 10 IoT CoreとWindows 10 IoT Enterpriseを統合する
- この統合されたバージョンは、Windows 10 IoT Enterpriseという名称になる予定(2021年)

### Windows 10 IoT Core OSセットアップ手順
参考：https://docs.microsoft.com/en-us/windows/iot-core/tutorials/rpi
1. Windows 10 IoT Core ダッシュボード インストール
- https://docs.microsoft.com/ja-jp/windows/iot-core/downloads
- Get Windows 10 IoT Core Dashboard をクリックしてダウンロード
- ダウンロード失敗した場合は、exeを"管理者として実行"をする
2. [新しいデバイスのセットアップ]を押します
3. SDカードにインストール
- デバイスの種類：Broadcomm[Raspberry Pi2&3]
- OSビルド：Windows10 IoT Core(17763)
- ドライブ：H
- デバイス名：RaspberryPi3
- 新しい管理者パスワード：****
- 管理者パスワードの確認入力：****
- [ダウンロードとインストール]を押します
- Windows10IoTCoreをダウンロードしていますが15分程度、その後SDカードへの書き込みがおこなわれる
- SDカードは準備が完了しています メッセージが表示されます。
- K:Data H:EEIESP L:MainOS がでてくる
4.電源オンするとWindowsが開始されます
- 最初は黒い画面にBootMgrが出ておかしかったが、ラズパイを電源オフオンを数回繰り返したら正常に起動した
- wifiに接続する 2.4GHzのみ。5GHZはダメみたい
- Team microSDHCカード 16GB 高速転送UHS-1を使ったら正常に起動しなかった。東芝のでOK。相性の悪いSDカードがあるのか？
5. setup.exeを管理者として実行して、
- [自分のデバイス]を見ると、RasberryPi3が見つかる。
- 最初見つからなかったが、こちらもラズパイを電源オフオンしたら見つかるようになった
 
### アプリ作成準備
以下の作業を必ずおこなうこと
- IoT Dashboardで[自分のデバイス]を開き、右クリックメニューの[デバイスポータルで開く]を選択
- Remoteメニューのデバイス側のEnable Windows IoT Remote Serverを[有効]にする
- 開発PC側で、開発者向け設定を検索して、表示されたウィンドウで、[開発者モード]を選択

### サンプルコードの実行
- サンプル集(https://github.com/microsoft/Windows-iotcore-samples/tree/develop/Samples）
- 動画(https://docs.microsoft.com/ja-jp/windows/iot-core/tutorials/quickstarter/devicesetup#using-the-iot-dashboard-raspberry-pi-minnowboard-nxp)

#### サンプル：Blinky
トラブルシューティング
- VisualStudioは更新プログラム
- 次のプロジェクトでは、プラットフォームSDK（UAP,Version＝10.0.17763.0）が必要です
- いったんターゲットを落とすこともできるというので、それにした。
- VSで配置が失敗する場合、出力を確認する。
- バージョンが合わないと言っていたら、プロジェクトのプロパティで アプリケーションのターゲットバージョンの最小バージョンを下げたらOKになった。
- 今度は、"There is no GPIO controller on this device."になってしまう問題。は、http://192.168.0.115:8080/#Devicesで接続して表示されるDevicesのDefault Contrller DriverのコンボボックスでInboxDriverを選択することで解決
- 配置が失敗する場合（ラズパイ側のリモートを有効にしましたか？）
- VisualStudioのプロパティーアプリケーションーターゲットー最小バージョンをWindows10 version1819 ビルド17763
- その後、デバッグの認証モードがWindowsに初期されてしまうので、ユニバーサルに変更すること

#### UWPでグラフ(LiveCharts編)
参考サイト：https://qiita.com/myasu/items/e8980be544761d668a82#livecharts%E3%83%91%E3%83%83%E3%82%B1%E3%83%BC%E3%82%B8%E3%82%92%E3%82%A4%E3%83%B3%E3%82%B9%E3%83%88%E3%83%BC%E3%83%AB
- [Nugetパッケージの管理]－[参照]を選択し[LiveCharts]を検索
- LiveCharts.Uwpを選択しインストール

## OSに Raspberry Pi OS を使う場合

### Raspberry Pi OSとは

参考サイト(https://www.raspberrypi.org/software/)

- Windows 10 IoT は、Windows 10 ファミリのメンバーで主に組み込みをターゲット
- Windows 10 IoT Core と Windows 10 IoT Enterprise の2つのエディション
- Windows 10 IoT Enterpriseは、ほぼPC版と同じで、x86(x64)のみサポート
- Windows 10 IoT Core は、UWP UIのみサポート(1度に1つのみ)し、ARMをサポートする。Raspberry Piは 2と3のみをサポート
- Windowsの次の長期サポート（LTS）リリースでは、Windows 10 IoT CoreとWindows 10 IoT Enterpriseを統合する
- この統合されたバージョンは、Windows 10 IoT Enterpriseという名称になる予定(2021年)

### Raspberry Pi OSセットアップ手順
参考サイト(https://www.raspberrypi.org/software/)

1. Raspberry Pi Imager をインストール
2. [Raspberry Pi OS [32bit]]を選択します
3. SDカードにインストール
4. Raspberry PiにSDカードをさして電源オンする
- このときサイズの小さい液晶（約５インチ）を使っていると画面にしましまがでるだけで正常に起動しなかった。解像度が合わないためだったのか、通常のPCで使うサイズのモニタに変更したところ正常に起動するようになった。
5. 各種初期設定をウィザードにしたがってすすめるだけで作業完了

### C#アプリの動かし方
- monoを使えばいける？
  - https://www.buildinsider.net/small/raspisignalr/01


# その他
## SDカードに別のOSを入れたい場合(切られたパーティションを削除)
1. コマンドプロンプトを起動。コマンドプロンプトは、管理者として実行を選ぶ。
2. diskpartコマンドを実行 C:\Users\xxx>diskpart
3. ディスクの一覧を表示して、パーティションを削除したいディスクを選ぶ。DISKPART>list disk
4. 一覧から操作をしたいUSBメモリを選ぶ。DISKPART>select disk ディスク番号
5. cleanコマンドで、構成情報を削除する。DISKPART>clean
6. プライマリーパーティションを作成する。DISKPART>create partition primary
7. DISKPARTコマンドを終了する。DISKPART>exit
- Windows10のExplorerではフォーマットはしない法がよさそう？
- FAT32でフォーマットしたあとイメージかいたらうまく起動しなかった

