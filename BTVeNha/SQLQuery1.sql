create database QLSV
go
use QLSV
GO
create table Lop
(
	Malop char(3) primary key,
	Tenlop nvarchar(30) Not Null,
	Siso int
)
create table Sinhvien 
(
	MaSV char(6) primary key,
	HotenSV nvarchar(40),
	Ngaysinh datetime,
	Gioitinh nvarchar(5),
	Hocbong Decimal(9,2),
	Malop char(3) Not Null foreign key references dbo.Lop(Malop)
)
---Malop Tenlop Siso
insert into Lop values ('TA1',N'Tiếng anh 5','56')
insert into Lop values ('LT2',N'lập trình windown','38')
--MaSV HotenSV Ngaysinh Gioitinh Hocbong Malop
insert into Sinhvien values ('SV0001',N'Phùng Đại Học','7/5/2001','Nam','500000','TA1')
insert into Sinhvien values ('SV0002',N'Lê Hữu Quân','6/8/2001','Nam','400000','LT2')
insert into Sinhvien values ('SV0003',N'Nguyễn Thị Trúc Phương','11/25/2002',N'Nữ','800000','TA1')
insert into Sinhvien values ('SV0004',N'Nguyễn Minh Tú','4/16/2001','Nam','600000','LT2')
 
 select * from SinhVien
  select * from Lop



 drop table Lop
 drop table Sinhvien

 select Sinhvien.MaSV, Sinhvien.HotenSV,Sinhvien.Ngaysinh,Sinhvien.Gioitinh,Sinhvien.Hocbong,Lop.Tenlop from Sinhvien ,Lop where Sinhvien.Malop = Lop.Malop
 select Tenlop from Lop

 --======================================
  select * from SinhVien
  select * from Lop



 drop table Lop
 drop table Sinhvien

 select Sinhvien.MaSV, Sinhvien.HotenSV,Sinhvien.Ngaysinh,Sinhvien.Gioitinh,Sinhvien.Hocbong,Lop.Tenlop from Sinhvien ,Lop where Sinhvien.Malop = Lop.Malop
 select Tenlop from Lop