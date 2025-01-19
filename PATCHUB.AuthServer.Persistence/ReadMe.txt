......................... :::::::::::::: GENEL B�LG�LER START ::::::::::: ......................


Herhangi bir i�lem yapmadan �nce migration� aktif etmek i�in: (Sadece entityframework s�r�m� i�in ge�erlidri Core�da gerek yok)
enable-migrations

Yapt���m�z de�i�iklikleri eklemek i�in:
add-migration "MigrationName"

herhangi bir de�i�iklik sonras� veritaban�na yans�tmak i�in:
update-database

de�i�iklikleri yapt�k veritaban�na yans�tt�k e�er ki geri bu yapt���m�z de�i�iklikleri belli bir migrationa geri d�nd�rmek i�in:
update-database -TargetMigration:"D�nece�iniz migration ismi"

E�er yapt���n�z b�t�n de�i�ikllikleri geri almak istiyorsan�z:
update-database -TargetMigration:0

Bu i�lemler sonras� migration� elle de silebilirsiniz, migration ismi vermezseniz son migration silinir.
remove-migration "Silinecek Migration"

Migration� olu�turup g�ncellediniz fakat bir de�i�iklik eklemeyi unutmu�sunuz bunun i�in silip ba�tan eklemeye gerek yok olu�turmu� oldu�unuz migration ad� ile ��yle yapabilrsiniz:
add-migration -force "VarOlanMigration"

ben bunlarla hi� u�ra�mayay�m yapt���m de�i�iklikler otomatik tan�mlans�n derseniz bunun i�inde bir y�ntem var
enable-migrations �EnableAutomaticMigration:$true

e�er �oklu Context s�n�f�n�z var ise -Context etiketi ve -Outpudir ile migrations klas�r�n� belirtmeniz gerekir
Add-Migration Migration_Name -Context ContextName -OutputDir Migrations\SqlServerMigrations


......................... :::::::::::::: GENEL B�LG�LER END  ::::::::::: ......................



 ..................... ::::::::::::::: PATCHUB GENEL KULLANIM START :::::::::::: ....................

Add-Migration InitialCreate -Context AuthDbContext -OutputDir Migrations/AuthDb
Update-Database -Context AuthDbContext


Add-Migration InitialCreate -Context AppDbContext -OutputDir Migrations/AppDb
Update-Database -Context AppDbContext
 ..................... ::::::::::::::: PATCHUB GENEL KULLANIM END :::::::::::: ....................