### READ Run Before Make Any Question (THIS IS FOR SWCYEN - A BLOODY IDIOT)
# TODO
	-Bang Nhap Sach (Check)
		+ Idea:  co 5 field: Id bang, Id sach, So luong, Ngay nhap, Gia nhap tu id sach truy ra cac thong tin cua sach nguoi lam viec chi can nhap id sach la xong (failed idead)
		+ Idea:  co 5 field: Id bang, TenSach, TenTacGia, TheLoai, So luong Nhap, Ngay nhap tu TenSach TenTacGia TheLoai truy ra IdSach roi cap nhat
		+ When value come from form, check amount import > 100 and check sachId exist and Sach.Amount < 200 then Sach.Amount += AmountImport 
		+ Dynamic de sau 
	- Add Authentication (check)
	- Add Authorization For User (check)
	- Add Authorization For Manager (check)
	- Manager can Add, Delete, Update, View All (check)
	- User can View From Display (check)
	- Add Phieu Thu Tien (check)
	- Add Hoa Don (check)
	- Mangager can Add but can't Delete, Update, View All in NhapSaches
	- Admin can View everything
# Daily (delete in the next day)
	- Add Bao Cao Ton Kho
		+ Idea: Sach, Ton Dau Thang, Phat Sinh, Ton Cuoi Thang
		+ Add sum after import to import sach
		+ Add Check time for sum
		+ Tinh First of month import = latest of month import of sach - all bill before start of month from the latest import day
		+ Phat Sinh = Tong so nhap trong thang (NhapSach)
		+ Ton Cuoi Thang = Trong thang nay Sach.Amount
# Rant
	- CLM bo m init la cai migration chac 50 me roi
	- TonSachDauThang = 
# Run
	- Lan Dau Chay He Thong
		+ Doi Connection String trong file appsettings.json Server = <TenServerCuaBan> , Database = <TenDatabaseCuaBan> , (User Id= sa , Password = <MatKhauCuaBan>)* - * Xoa duoc 
		+ Chon Tools -> Nuget Package Manager -> Package Manager Console
			+ Chay 2 Lenh Sau
				+ Add-Migration InitialCreate
				+ Update-Database
		+ Bam nut Start trong Visual Studio 2022
		+ Register 3 User (Day la vi du, co the thay doi)
			+ Username: admin, Password: Admin1!
			+ Username: user, Password: User12! 
			+ Username: manager, Password: Manager1!
		+ Mo Sql len, tim:
			+ [dbo].[AspNetUsers] 
			+ [dbo].[AspNetRoles]
				* Then 2 role: (chon edit top 200 rows)
					- 1, Admin,ADMIN,NULL
					- 2, User,USER,NULL
					-3, Manager,MANAGER,NULL
			+ [dbo].[AspNetUserRoles]
				* Then 2 role: (chon edit top 200 rows)
					- Id cua Admin trong bang [dbo].[AspNetUsers], 1 
					- Id cua User trong bang [dbo].[AspNetUsers], 2
					- Id cua Manager trong bang [dbo].[AspNetUsers], 3
		+ Restart APP
# Playground
	- First have add