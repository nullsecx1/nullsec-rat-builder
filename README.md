# nullsec-rat-builderNullSec RAT & Info Stealer Builder
Özellikler
Kullanıcı adı, Bilgisayar adı, İşletim sistemi sürümü, Local IP, External IP toplanır

Ekran görüntüsü alınır

Discord token’ları LevelDB’den taranır

Masaüstünden .txt, .docx, .pdf dosyaları kopyalanır

Veriler ziplenip SMTP (Gmail) ile belirlenen mail adresine düzenli aralıklarla gönderilir

Persistence (kalıcılık) sağlanır (%AppData% altında kendini kopyalar ve başlangıca eklenir)

USB yayılımı yapılır (takılan USB sürücülere gizli dosya olarak kopyalanır)

RAT özellikleri: Uzak komut alabilir, veri gönderebilir, komut paneli ile kontrol sağlanabilir

Builder: Kullanıcıdan bir sunucu URL’si alır, virüs dosyasına bu URL’yi yerleştirir, virüs verileri bu URL’ye gönderir

Kullanım
template.cs dosyasındaki {{SERVER_URL}} placeholder’ını builder programı ile verilen URL ile değiştir

Builder.exe derlenir ve kurbanlara bu virüs dosyası gönderilir

Virüs, hedef sistemde bilgileri toplar ve belirlenen URL’ye gönderir

Web paneli üzerinden uzak komut gönderme ve sonuçları alma işlemleri yapılabilir

Kurulum
Projeyi klonlayın veya indirin

builder.cs derleyip çalıştırarak virüs dosyasını üretin

Virüsü hedef makinaya yayıp verileri web panelinizden takip edin

⚠️ Not
Bu projede yapay zeka (ChatGPT) yardımı kullanılmıştır.
Kodlarda hatalar veya eksiklikler olabilir.
Geri bildirim ve geliştirmeler için iletişime geçebilirsiniz.

