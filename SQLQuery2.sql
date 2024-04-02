--SELECT package.Maximum_KM,package.ExtraKM_cost,package.Maximum_Hour,package.ExtraHour_Cost,Hire_Tariff.Standard_rate from Hire_Tariff

select * from Hire_Tariff;
select * from package;

select p.Maximum_KM,p.ExtraKM_cost,p.Maximum_Hour,p.ExtraHour_Cost,ht.Standard_rate  from package AS P inner join Hire_Tariff as ht on p.Type=ht.Package_Type where p.Package_ID='' and ht.Package_Type='' and ht.Vehicle_Type='';
select Standard_rate from Hire_Tariff where Vehicle_Type = ''  and Package_Type = '';
select Maximum_KM, ExtraKM_cost,Maximum_Hour,ExtraHour_Cost from package where Package_ID = '';