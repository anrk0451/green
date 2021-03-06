--1.生成墓区
 declare 
   cursor cur_region is select * from newtomb.region where curstatus = '1' order by sortId;                       
 begin
   for rec_1 in cur_region loop
     insert into rg01 (rg001, rg002, rg003,   rg009, price)
     values(
        pkg_business.fun_EntityPk('RG01'),
        '1',
        rec_1.regionDesc,
        '0000000000',
        0
     );
   end loop;
 end;   
 
--生成墓区-排
 declare
   cursor cur_rg01 is select * from rg01 where rg002 = '1'  order by rg001;
 begin
   for rec_1 in cur_rg01 loop
     insert into rg01(rg001,rg002,rg003,rg004,rg005,rg006,rg007,rg009,price)
       select * from 
       (
         select  pkg_business.fun_EntityPk('RG01'),
                 '2',
                 ROWDESC,
                 rec_1.rg004,
                 startbit,
                 endbit,
                 direction,
                 rec_1.rg001,
                 rec_1.price
            from newtomb.rower
           where regionId = (select regionId from 墓区映射 where rg001 = rec_1.rg001)
           order by ORDERID
        );
   end loop;
 end;
