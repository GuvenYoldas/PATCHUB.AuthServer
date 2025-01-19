......................... :::::::::::::: GENEL BÝLGÝLER START ::::::::::: ......................


Herhangi bir iþlem yapmadan önce migrationý aktif etmek için: (Sadece entityframework sürümü için geçerlidri Core’da gerek yok)
enable-migrations

Yaptýðýmýz deðiþiklikleri eklemek için:
add-migration "MigrationName"

herhangi bir deðiþiklik sonrasý veritabanýna yansýtmak için:
update-database

deðiþiklikleri yaptýk veritabanýna yansýttýk eðer ki geri bu yaptýðýmýz deðiþiklikleri belli bir migrationa geri döndürmek için:
update-database -TargetMigration:"Döneceðiniz migration ismi"

Eðer yaptýðýnýz bütün deðiþikllikleri geri almak istiyorsanýz:
update-database -TargetMigration:0

Bu iþlemler sonrasý migrationý elle de silebilirsiniz, migration ismi vermezseniz son migration silinir.
remove-migration "Silinecek Migration"

Migrationý oluþturup güncellediniz fakat bir deðiþiklik eklemeyi unutmuþsunuz bunun için silip baþtan eklemeye gerek yok oluþturmuþ olduðunuz migration adý ile þöyle yapabilrsiniz:
add-migration -force "VarOlanMigration"

ben bunlarla hiç uðraþmayayým yaptýðým deðiþiklikler otomatik tanýmlansýn derseniz bunun içinde bir yöntem var
enable-migrations –EnableAutomaticMigration:$true

eðer çoklu Context sýnýfýnýz var ise -Context etiketi ve -Outpudir ile migrations klasörünü belirtmeniz gerekir
Add-Migration Migration_Name -Context ContextName -OutputDir Migrations\SqlServerMigrations


......................... :::::::::::::: GENEL BÝLGÝLER END  ::::::::::: ......................



 ..................... ::::::::::::::: PATCHUB GENEL KULLANIM START :::::::::::: ....................

Add-Migration InitialCreate -Context AuthDbContext -OutputDir Migrations/AuthDb
Update-Database -Context AuthDbContext


Add-Migration InitialCreate -Context AppDbContext -OutputDir Migrations/AppDb
Update-Database -Context AppDbContext
 ..................... ::::::::::::::: PATCHUB GENEL KULLANIM END :::::::::::: ....................