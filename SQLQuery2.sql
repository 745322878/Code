
insert into T_Student(Name,Class,Number,TelPhone) values('����','����1501','04153102','13720517934')

delete from T_Student where Name='����'

select * from T_Student

select Name,Class,Number from T_Student

select * from T_Student where Number>04143102

update T_Student set Hobbies='�Է�' where Number>04143080

update T_Student set Hobbies='�򶹶�' where Number<04143000

select Name as c1 ,1 as c2,Number as c3 from T_Student

select name,class,'haha' as haha,GETDATE() as LocalTime from T_Student

select  Max(Number) from T_Student

select Min(Number) from T_Student

select Max(Number) as maxnumber,Min(Number) as minnumber,COUNT(*) as Row from T_Student

ALTER TABLE T_Student add Score int default 0

alter table T_Student add Age int 

ALTER TABLE T_Student drop column Age

select * from T_Student

update T_Student set Score=0

update T_Student set Score=60 where Id=11

select Max(Score) as maxscore,Min(Score) as minscore,AVG(Score) as aveger from T_Student

select Count(*) as row from T_Student where Score>60

select Min(Score) as minscore from T_Student where Score>60

update T_Student set Number='04141197',TelPhone='15667023326',Class='�ƿ�1406' where Id=6 

select * from T_Student where Name like '��%'

select * from T_Student where Name like '��%��'

select * from T_Student where Class like '%14%'

select * from T_Student where Class like '%���%'

select * from T_Student order by Score   --Ĭ�ϴ�С�������

select * from T_Student order by Score Desc   --����Descending

select * from T_Student order by Score Desc,Number Desc,Name Desc

select * from T_Student where Class like '%���%'  order by Score Desc

select * from T_Student 


insert into T_Student(Name,Class,Number,TelPhone, Score) 
output inserted.Id values('����','���1502','04153107','13720517936','78')

delete from T_Student where Id=16