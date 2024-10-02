# Kompanion

### Öncelikle zaman ayırdığınız için teşekkür ederim. Aşağıda projeye ait bazı notlarım olacak, dilerseniz göz atabilirsiniz :)

> Projeyi geliştirirken clean architecture'dan esinlenerek geliştirdim. Bir süredir üzerinde çalıştığım Workable.Architecture isimli github repolarımın içerisinde bulunan projeden template yaratarak projeye başladım. Bundan dolayı proje içerisinde kullanmadığım kısımlar olabilir şimdiden kusura bakmayın.
>> + Workable.Architecture projemi görüp onu da incelemek isterseniz bana ulaşmanız yeterli olacaktır. İstediğiniz herhangi bir kişiye github üzerinden projeyi paylaşabilirim.
>> + Proje içerisinde kod tekrarları veya anlamsız gelen yerler görebilirsiniz, zamanım kısıtlı olduğundan dolayı ve hali hazırda çalıştığımdan dolayı bazı yerlerde kod tekrarlarına maalesef ki kaçtım.
>> + Proje içerisinde MediatR pattern'i kullanarak tüm requestleri ve responseları behaviorlardan geçirerek tüm süreci loglamaya çalıştım aynı zamanda da exceptionları yakalamaya çalıştım.
>> + FluentValidation kütüphanesi ile bazı request objelerine validation'lar uyguladım örnek olması açısından.
>> + Moq ve xUnit kütüphaneleri kullanarak testler yazdım bu testleri BuildingBlocks klasörünün altında görebilirsiniz. Sadece BuildingBlocks altında olmasının nedeni template projemden geliyor ama dilerseniz handlerlar ve repositoryler için de test yazabilirim.
>> + Cache olarak redis kullandım ve ICacheRequest interface'ini alan tüm requestler cachelenerek sonrasında cacheden getiriliyor. Örneğini GetProductQuery de görebilirsiniz.
>> + Log olarak Serilog kütüphanesini kullandım ve Graylog üzerinden görüntülemeler sağladım.
>> + MySQL, Redis ve Graylog'a ait docker compose dosyalarını proje dizininde bulunan Composers klasörü içerisinden ulaşabilirsiniz. (Logları görüntülemek için Graylog arayüzünden bazı ayarlamalar yapmak gerekir.)
>> + Proje dizininde common.props dosyası göreceksiniz, bu dosya tüm projelerin csproj'unu eklenmiş durumda ve bu dosya sayesinde TargetFramework, ImplicitUsings, LangVersion gibi kısımları tek yerden yönetebiliyorum.
>> + PaymentRuleEntity class'ının içerisinde kullandığım CreatePaymentRuleBusinessRule ise entity oluşurken entity'e ait kuralları kontrol edip gerikirse işlemi sonlandırma görevi vardır.

> ### Gözümdem kaçan veya atladığım bir durum olursa lütfen beni bilgilendiriniz.

