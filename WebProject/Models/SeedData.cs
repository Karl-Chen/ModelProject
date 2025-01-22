using Microsoft.EntityFrameworkCore;

namespace WebProject.Models
{
    //1.3.3 撰寫SeedData類別的內容
    //      (1)撰寫靜態方法 Initialize(IServiceProvider serviceProvider)
    //      (2)撰寫Book及ReBook資料表內的初始資料程式
    //      (3)撰寫getFileBytes，功能為將照片轉成二進位資料
    public class SeedData
    {
        //(1)撰寫靜態方法 Initialize(IServiceProvider serviceProvider)

        //private readonly GuestBookContext _context;
        //public SeedData(GuestBookContext context)
        //{
        //    _context = context;
        //}
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (GuestModelContext _context = new GuestModelContext(serviceProvider.GetRequiredService<DbContextOptions<GuestModelContext>>()))
            {
                //if (!_context.HandleOrder.Any())
                {
                    //供應商
                    //_context.Supplier.AddRange(
                    //        new Supplier
                    //        {
                    //            SupplierID = "01",
                    //            SupplierName = "萬榮",
                    //            ContactWindow = "萬榮業務",
                    //            Moblie = "02-2836-8689",
                    //            Addr = "台北市士林區德行西路7號10樓"
                    //        },
                    //        new Supplier
                    //        {
                    //            SupplierID = "02",
                    //            SupplierName = "雙緯",
                    //            ContactWindow = "雙緯業務",
                    //            Moblie = "02-26097772",
                    //            Addr = "新北市林口區麗園二街41號"
                    //        },
                    //        new Supplier
                    //        {
                    //            SupplierID = "03",
                    //            SupplierName = "振光",
                    //            ContactWindow = "振光業務",
                    //            Moblie = "02-29619288",
                    //            Addr = "新北市板橋區中山路一段 156號9樓A5"
                    //        },
                    //        new Supplier
                    //        {
                    //            SupplierID = "04",
                    //            SupplierName = "科利達",
                    //            ContactWindow = "科利達業務",
                    //            Moblie = "02-25471381",
                    //            Addr = "新北市林口區中華路505號13樓"
                    //        },
                    //    new Supplier
                    //    {
                    //        SupplierID = "05",
                    //        SupplierName = "宏駿國際有限公司",
                    //        ContactWindow = "宏駿業務",
                    //        Moblie = "02-2984-9988",
                    //        Addr = "新北市三重區重陽路3段192號3樓D室"
                    //    },
                    //    new Supplier
                    //    {
                    //        SupplierID = "06",
                    //        SupplierName = "固來有限公司",
                    //        ContactWindow = "固來業務",
                    //        Moblie = "02-28310903",
                    //        Addr = "台北市士林區磺溪街36-1號六樓"
                    //    },
                    //    new Supplier
                    //    {
                    //        SupplierID = "07",
                    //        SupplierName = "文苑貿易有限公司",
                    //        ContactWindow = "文苑業務",
                    //        Moblie = "06-2292196",
                    //        Addr = "台南市中區民權路二段173號"
                    //    });
                    //_context.SaveChanges();

                    //_context.Brand.AddRange(
                    //    new Brand
                    //    {
                    //        BrandID = "01",
                    //        BrandName = "萬代"
                    //    },
                    //    new Brand
                    //    {
                    //        BrandID = "02",
                    //        BrandName = "壽屋"
                    //    },
                    //    new Brand
                    //    {
                    //        BrandID = "03",
                    //        BrandName = "青島"
                    //    },
                    //    new Brand
                    //    {
                    //        BrandID = "04",
                    //        BrandName = "岩田"
                    //    },
                    //    new Brand
                    //    {
                    //        BrandID = "05",
                    //        BrandName = "郡氏"
                    //    },
                    //    new Brand
                    //    {
                    //        BrandID = "06",
                    //        BrandName = "田宮"
                    //    },
                    //    new Brand
                    //    {
                    //        BrandID = "07",
                    //        BrandName = "蓋亞"
                    //    },
                    //    new Brand
                    //    {
                    //        BrandID = "08",
                    //        BrandName = "AV"
                    //    },
                    //    new Brand
                    //    {
                    //        BrandID = "09",
                    //        BrandName = "AK"
                    //    },
                    //    new Brand
                    //    {
                    //        BrandID = "10",
                    //        BrandName = "H&S Infinity"
                    //    },
                    //    new Brand
                    //    {
                    //        BrandID = "11",
                    //        BrandName = "WAVE"
                    //    },
                    //    new Brand
                    //    {
                    //        BrandID = "12",
                    //        BrandName = "3M"
                    //    },
                    //    new Brand
                    //    {
                    //        BrandID = "13",
                    //        BrandName = "神之手"
                    //    },
                    //    new Brand
                    //    {
                    //        BrandID = "14",
                    //        BrandName = "OFLA"
                    //    });
                    //_context.SaveChanges();

                    //_context.ProductType.AddRange(
                    //    new ProductType
                    //    {
                    //        TypeID = "P",
                    //        TypeName = "模型"
                    //    },
                    //    new ProductType
                    //    {
                    //        TypeID = "T",
                    //        TypeName = "工具"
                    //    },
                    //    new ProductType
                    //    {
                    //        TypeID = "C",
                    //        TypeName = "耗材"
                    //    },
                    //    new ProductType
                    //    {
                    //        TypeID = "O",
                    //        TypeName = "週邊"
                    //    });
                    //_context.SaveChanges();

                    //_context.ProductSpecification.AddRange(
                    //    new ProductSpecification
                    //    {
                    //        SpecificationID = "MGEX",
                    //        SpecificationName = "MGEX"
                    //    },
                    //    new ProductSpecification
                    //    {
                    //        SpecificationID = "MGSD",
                    //        SpecificationName = "MGSD"
                    //    },
                    //    new ProductSpecification
                    //    {
                    //        SpecificationID = "PG",
                    //        SpecificationName = "PG及1/60"
                    //    },
                    //    new ProductSpecification
                    //    {
                    //        SpecificationID = "RG",
                    //        SpecificationName = "RG"
                    //    },
                    //    new ProductSpecification
                    //    {
                    //        SpecificationID = "MG",
                    //        SpecificationName = "MG及1/100"
                    //    },
                    //    new ProductSpecification
                    //    {
                    //        SpecificationID = "HG",
                    //        SpecificationName = "HG"
                    //    },
                    //    new ProductSpecification
                    //    {
                    //        SpecificationID = "EG",
                    //        SpecificationName = "EG"
                    //    },
                    //    new ProductSpecification
                    //    {
                    //        SpecificationID = "BB",
                    //        SpecificationName = "BB及SD"
                    //    },
                    //    new ProductSpecification
                    //    {
                    //        SpecificationID = "30MM",
                    //        SpecificationName = "30MM"
                    //    },
                    //    new ProductSpecification
                    //    {
                    //        SpecificationID = "MD",
                    //        SpecificationName = "女神裝置"
                    //    },
                    //    new ProductSpecification
                    //    {
                    //        SpecificationID = "FAG",
                    //        SpecificationName = "Frame Arms Girl"
                    //    },
                    //    new ProductSpecification
                    //    {
                    //        SpecificationID = "MMS",
                    //        SpecificationName = "武裝神姬"
                    //    },
                    //    new ProductSpecification
                    //    {
                    //        SpecificationID = "Blade",
                    //        SpecificationName = "刀片"
                    //    },
                    //    new ProductSpecification
                    //    {
                    //        SpecificationID = "Cut",
                    //        SpecificationName = "剪鉗"
                    //    },
                    //    new ProductSpecification
                    //    {
                    //        SpecificationID = "Drill",
                    //        SpecificationName = "鑽頭"
                    //    },
                    //    new ProductSpecification
                    //    {
                    //        SpecificationID = "WaterPaint",
                    //        SpecificationName = "水性漆"
                    //    },
                    //    new ProductSpecification
                    //    {
                    //        SpecificationID = "EnamelPaint",
                    //        SpecificationName = "珐瑯漆"
                    //    },
                    //    new ProductSpecification
                    //    {
                    //        SpecificationID = "NitrocellulosePaint",
                    //        SpecificationName = "硝基漆"
                    //    },
                    //    new ProductSpecification
                    //    {
                    //        SpecificationID = "Board",
                    //        SpecificationName = "膠板"
                    //    },
                    //    new ProductSpecification
                    //    {
                    //        SpecificationID = "Gule",
                    //        SpecificationName = "膠水"
                    //    },
                    //    new ProductSpecification
                    //    {
                    //        SpecificationID = "MicoPen",
                    //        SpecificationName = "鋼彈麥克筆"
                    //    },
                    //    new ProductSpecification
                    //    {
                    //        SpecificationID = "Sandpaper",
                    //        SpecificationName = "砂紙打磨板"
                    //    });
                    //_context.SaveChanges();

                    //_context.Prodect.AddRange(
                    //        new Product
                    //        {
                    //            ProductID = "PMGEX002",
                    //            ProductName = "MGEX攻擊自由",
                    //            Description = "最帥最強，將金色骨架特色以更加突出的方式完全展現出來的模型",
                    //            CostJP = 13800,
                    //            CostExchangeRate = 0.2f,
                    //            PriceExchangeRage = 0.23f,
                    //            Inventory = 3,
                    //            OrderedQuantity = 3,
                    //            ProductTypeID = "P",
                    //            ProductSpecificationID = "MGEX",
                    //            BrandID = "01",
                    //            SupplierID = "01",
                    //            Photo = "PIC_PMGEX002.jpg"
                    //        },
                    //        new Product
                    //        {
                    //            ProductID = "PMGEX001",
                    //            ProductName = "MGEX獨角獸",
                    //            Description = "自帶發光系統，將變身及發光以更加突出的方式完全展現出來的模型",
                    //            CostJP = 23000,
                    //            CostExchangeRate = 0.2f,
                    //            PriceExchangeRage = 0.23f,
                    //            Inventory = 3,
                    //            OrderedQuantity = 2,
                    //            ProductTypeID = "P",
                    //            ProductSpecificationID = "MGEX",
                    //            BrandID = "01",
                    //            SupplierID = "01",
                    //            Photo = "PIC_PMGEX001.jpg"
                    //        },
                    //        new Product
                    //        {
                    //            ProductID = "PMGSD001",
                    //            ProductName = "MGSD自由鋼彈",
                    //            Description = "以MG的骨架技術展現在SD的Size上，擁有迷人的身姿及高度可動展現出自由鋼彈的帥氣",
                    //            CostJP = 4290,
                    //            CostExchangeRate = 0.2f,
                    //            PriceExchangeRage = 0.25f,
                    //            ProductTypeID = "P",
                    //            Inventory = 0,
                    //            OrderedQuantity = 0,
                    //            ProductSpecificationID = "MGSD",
                    //            BrandID = "01",
                    //            SupplierID = "01",
                    //            Photo = "PIC_PMGSD001.jpg"
                    //        },
                    //        new Product
                    //        {
                    //            ProductID = "PMGSD002",
                    //            ProductName = "MGSD獵魔鋼彈",
                    //            Description = "以MG的骨架技術展現在SD的Size上，擁有迷人的身姿及高度可動展現出獵魔鋼彈帥氣骨架露出",
                    //            CostJP = 4290,
                    //            CostExchangeRate = 0.2f,
                    //            PriceExchangeRage = 0.25f,
                    //            Inventory = 13,
                    //            OrderedQuantity = 10,
                    //            ProductTypeID = "P",
                    //            ProductSpecificationID = "MGSD",
                    //            BrandID = "01",
                    //            SupplierID = "01",
                    //            Photo = "PIC_PMGSD002.jpg"
                    //        },
                    //        new Product
                    //        {
                    //            ProductID = "PMGSD003",
                    //            ProductName = "MGSD飛翼EW",
                    //            Description = "以MG的骨架技術展現在SD的Size上，展露出俗稱飛翼掉毛的帥氣天使",
                    //            CostJP = 4950,
                    //            CostExchangeRate = 0.2f,
                    //            PriceExchangeRage = 0.25f,
                    //            Inventory = 15,
                    //            OrderedQuantity = 15,
                    //            ProductTypeID = "P",
                    //            ProductSpecificationID = "MGSD",
                    //            BrandID = "01",
                    //            SupplierID = "01",
                    //            Photo = "PIC_PMGSD003.jpeg"
                    //        },
                    //        new Product
                    //        {
                    //            ProductID = "PPGU_001",
                    //            ProductName = "PGU-RX-78-2",
                    //            Description = "以1：60比例呈現最新技術結晶的究極鋼普拉，將初代元祖鋼彈完美呈現，達到完美分件，也讓玩家可以更便利的上色，即使不塗裝也是很帥氣",
                    //            CostJP = 4950,
                    //            CostExchangeRate = 0.2f,
                    //            PriceExchangeRage = 0.25f,
                    //            Inventory = 5,
                    //            OrderedQuantity = 5,
                    //            ProductTypeID = "P",
                    //            ProductSpecificationID = "PG",
                    //            BrandID = "01",
                    //            SupplierID = "01",
                    //            Photo = "PIC_PPGU_001.jpeg"
                    //        },
                    //        new Product
                    //        {
                    //            ProductID = "PPG__002",
                    //            ProductName = "PG-Zaku-夏亞專用薩克",
                    //            Description = "以1：60比例呈現將夏亞專用薩克完美呈現",
                    //            CostJP = 12000,
                    //            CostExchangeRate = 0.2f,
                    //            PriceExchangeRage = 0.23f,
                    //            Inventory = 3,
                    //            OrderedQuantity = 1,
                    //            ProductTypeID = "P",
                    //            ProductSpecificationID = "PG",
                    //            BrandID = "01",
                    //            SupplierID = "01",
                    //            Photo = "PIC_PPG__002.jpeg"
                    //        },
                    //        new Product
                    //        {
                    //            ProductID = "PPGCE003",
                    //            ProductName = "PG攻擊鋼彈",
                    //            Description = "以1：60比例呈現將攻擊鋼彈完美呈現，以超高可動及外甲展開做為賣點",
                    //            CostJP = 14000,
                    //            CostExchangeRate = 0.2f,
                    //            PriceExchangeRage = 0.23f,
                    //            Inventory = 19,
                    //            OrderedQuantity = 12,
                    //            ProductTypeID = "P",
                    //            ProductSpecificationID = "PG",
                    //            BrandID = "01",
                    //            SupplierID = "01",
                    //            Photo = "PIC_PPGCE003.jpeg"
                    //        },
                    //        new Product
                    //        {
                    //            ProductID = "PPGCE004",
                    //            ProductName = "PG空中霸者",
                    //            Description = "以1：60比例將攻擊鋼彈的翔翼背包搭載在空中霸者上，並且可以將翔翼背包與PG攻擊鋼彈相結合",
                    //            CostJP = 5000,
                    //            CostExchangeRate = 0.2f,
                    //            PriceExchangeRage = 0.23f,
                    //            Inventory = 5,
                    //            OrderedQuantity = 3,
                    //            ProductTypeID = "P",
                    //            ProductSpecificationID = "PG",
                    //            BrandID = "01",
                    //            SupplierID = "01",
                    //            Photo = "PIC_PPGCE004.jpeg"
                    //        },
                    //        new Product
                    //        {
                    //            ProductID = "PPGCE005",
                    //            ProductName = "PG攻擊自由",
                    //            Description = "以1：60比例將攻擊自由的金色骨架及超大超華麗翅膀呈現在玩家",
                    //            CostJP = 25000,
                    //            CostExchangeRate = 0.2f,
                    //            PriceExchangeRage = 0.23f,
                    //            Inventory = 12,
                    //            OrderedQuantity = 9,
                    //            ProductTypeID = "P",
                    //            ProductSpecificationID = "PG",
                    //            BrandID = "01",
                    //            SupplierID = "01",
                    //            Photo = "PIC_PPGCE005.jpeg"
                    //        },
                    //        new Product
                    //        {
                    //            ProductID = "PPGCE006",
                    //            ProductName = "PG紅異端改",
                    //            Description = "以1：60比例將紅異端改的肌肉特色造型呈現，其中更以雙武士刀及戰術大劍做為賣點",
                    //            CostJP = 24840,
                    //            CostExchangeRate = 0.2f,
                    //            PriceExchangeRage = 0.23f,
                    //            Inventory = 33,
                    //            OrderedQuantity = 30,
                    //            ProductTypeID = "P",
                    //            ProductSpecificationID = "PG",
                    //            BrandID = "01",
                    //            SupplierID = "01",
                    //            Photo = "PIC_PPGCE006.jpeg"
                    //        },
                    //        new Product
                    //        {
                    //            ProductID = "PPG00007",
                    //            ProductName = "PG能天使(含燈組)",
                    //            Description = "能天使以1：60比例呈現出來，並且可以裝上燈組做為發光的賣點",
                    //            CostJP = 34560,
                    //            CostExchangeRate = 0.2f,
                    //            PriceExchangeRage = 0.23f,
                    //            Inventory = 7,
                    //            OrderedQuantity = 3,
                    //            ProductTypeID = "P",
                    //            ProductSpecificationID = "PG",
                    //            BrandID = "01",
                    //            SupplierID = "01",
                    //            Photo = "PIC_PPG00007.jpeg"
                    //        },
                    //        new Product
                    //        {
                    //            ProductID = "PPGUC008",
                    //            ProductName = "PG獨角獸(燈組另購)",
                    //            Description = "獨角獸以1：60比例呈現出來，並且可以另購燈組裝上做為發光的賣點",
                    //            CostJP = 34560,
                    //            CostExchangeRate = 0.2f,
                    //            PriceExchangeRage = 0.23f,
                    //            Inventory = 7,
                    //            OrderedQuantity = 5,
                    //            ProductTypeID = "P",
                    //            ProductSpecificationID = "PG",
                    //            BrandID = "01",
                    //            SupplierID = "01",
                    //            Photo = "PIC_PPGUC008.jpeg"
                    //        },
                    //        //---------------------------以下MG----------------
                    //        new Product
                    //        {
                    //            ProductID = "PMG__175",
                    //            ProductName = "MG MSN-04 Sazabi Ver. Ka",
                    //            Description = "由Ka老師重新設計水貼貼在沙薩比上，更加增添沙薩比細節",
                    //            CostJP = 9000,
                    //            CostExchangeRate = 0.2f,
                    //            PriceExchangeRage = 0.23f,
                    //            Inventory = 28,
                    //            OrderedQuantity = 24,
                    //            ProductTypeID = "P",
                    //            ProductSpecificationID = "MG",
                    //            BrandID = "01",
                    //            SupplierID = "01",
                    //            Photo = "PIC_PMG__175.jpeg"
                    //        },
                    //        new Product
                    //        {
                    //            ProductID = "PMG__163",
                    //            ProductName = "MG RX-93 Nu Gundam Ver. Ka",
                    //            Description = "由Ka老師重新設計水貼貼在牛鋼上，更加增添牛鋼細節",
                    //            CostJP = 7000,
                    //            CostExchangeRate = 0.2f,
                    //            PriceExchangeRage = 0.23f,
                    //            Inventory = 33,
                    //            OrderedQuantity = 28,
                    //            ProductTypeID = "P",
                    //            ProductSpecificationID = "MG",
                    //            BrandID = "01",
                    //            SupplierID = "01",
                    //            Photo = "PIC_PMG__163.jpeg"
                    //        },
                    //        new Product
                    //        {
                    //            ProductID = "PMG__169",
                    //            ProductName = "MG GAT-X105 Aile Strike Gundam Ver. RM",
                    //            Description = "翔翼型攻擊鋼彈",
                    //            CostJP = 4200,
                    //            CostExchangeRate = 0.2f,
                    //            PriceExchangeRage = 0.23f,
                    //            Inventory = 22,
                    //            OrderedQuantity = 17,
                    //            ProductTypeID = "P",
                    //            ProductSpecificationID = "MG",
                    //            BrandID = "01",
                    //            SupplierID = "01",
                    //            Photo = "PIC_PMG__169.jpeg"
                    //        },
                    //        new Product
                    //        {
                    //            ProductID = "PMG__172",
                    //            ProductName = "MG RX-78-2 Gundam ver. 3.0",
                    //            Description = "初鋼3.0",
                    //            CostJP = 4500,
                    //            CostExchangeRate = 0.2f,
                    //            PriceExchangeRage = 0.23f,
                    //            Inventory = 25,
                    //            OrderedQuantity = 21,
                    //            ProductTypeID = "P",
                    //            ProductSpecificationID = "MG",
                    //            BrandID = "01",
                    //            SupplierID = "01",
                    //            Photo = "PIC_PMG__172.jpeg"
                    //        },
                    //        new Product
                    //        {
                    //            ProductID = "PMG__223",
                    //            ProductName = "MG MSN-06S-2 Sinanju Stein [Narrative Ver.] Ver.Ka",
                    //            Description = "新安洲原石卡版",
                    //            CostJP = 8000,
                    //            CostExchangeRate = 0.2f,
                    //            PriceExchangeRage = 0.23f,
                    //            Inventory = 20,
                    //            OrderedQuantity = 15,
                    //            ProductTypeID = "P",
                    //            ProductSpecificationID = "MG",
                    //            BrandID = "01",
                    //            SupplierID = "01",
                    //            Photo = "PIC_PMG__223.jpeg"
                    //        },
                    //        //--------------------------以下RG
                    //        new Product
                    //        {
                    //            ProductID = "PRG__011",
                    //            ProductName = "RG ZGMF-X42S Destiny Gundam",
                    //            Description = "RG 命運，劇場版捥回了一切",
                    //            CostJP = 2500,
                    //            CostExchangeRate = 0.2f,
                    //            PriceExchangeRage = 0.23f,
                    //            Inventory = 15,
                    //            OrderedQuantity = 5,
                    //            ProductTypeID = "P",
                    //            ProductSpecificationID = "RG",
                    //            BrandID = "01",
                    //            SupplierID = "01",
                    //            Photo = "PIC_PRG__011.jpeg"
                    //        },
                    //        new Product
                    //        {
                    //            ProductID = "PRG__018",
                    //            ProductName = "RG GN-0000-GNR-010 OO Raiser",
                    //            Description = "RG 00 強化模組的神棍機",
                    //            CostJP = 3000,
                    //            CostExchangeRate = 0.2f,
                    //            PriceExchangeRage = 0.23f,
                    //            Inventory = 15,
                    //            OrderedQuantity = 5,
                    //            ProductTypeID = "P",
                    //            ProductSpecificationID = "RG",
                    //            BrandID = "01",
                    //            SupplierID = "01",
                    //            Photo = "PIC_PRG__018.jpeg"
                    //        },
                    //        new Product
                    //        {
                    //            ProductID = "PRG__021",
                    //            ProductName = "RG GNT-0000 OO Qan[T]",
                    //            Description = "RG 00 會量子化的神棍機",
                    //            CostJP = 2500,
                    //            CostExchangeRate = 0.2f,
                    //            PriceExchangeRage = 0.23f,
                    //            Inventory = 15,
                    //            OrderedQuantity = 5,
                    //            ProductTypeID = "P",
                    //            ProductSpecificationID = "RG",
                    //            BrandID = "01",
                    //            SupplierID = "01",
                    //            Photo = "PIC_PRG__021.jpeg"
                    //        },
                    //    //產品先建到這裡，後續再回來建，不然做不完了
                    //    //以下田宮耗材
                    //    new Product
                    //    {
                    //        ProductID = "TTAC_001",
                    //        ProductName = "田宮斜口鉗74035",
                    //        Description = "田宮出品高級斜口鉗，雙刃薄刃鉗，效果略輸單刃的神之手一點",
                    //        CostJP = 3100,
                    //        CostExchangeRate = 0.22f,
                    //        PriceExchangeRage = 0.25f,
                    //        Inventory = 15,
                    //        OrderedQuantity = 7,
                    //        ProductTypeID = "T",
                    //        ProductSpecificationID = "Cut",
                    //        BrandID = "06",
                    //        SupplierID = "02",
                    //        Photo = "PIC_TTAC_001.jpg"
                    //    },
                    //    new Product
                    //    {
                    //        ProductID = "TTAC_002",
                    //        ProductName = "田宮斜口鉗74123",
                    //        Description = "田宮出品高級斜口鉗，雙刃薄刃鉗，刃比74035薄一點，因此較為脆弱",
                    //        CostJP = 3467,
                    //        CostExchangeRate = 0.22f,
                    //        PriceExchangeRage = 0.25f,
                    //        Inventory = 15,
                    //        OrderedQuantity = 7,
                    //        ProductTypeID = "T",
                    //        ProductSpecificationID = "Cut",
                    //        BrandID = "06",
                    //        SupplierID = "02",
                    //        Photo = "PIC_TTAC_123.jpg"
                    //    },
                    //    new Product
                    //    {
                    //        ProductID = "TGOD_003",
                    //        ProductName = "神之手 SPN-120",
                    //        Description = "模型斜口鉗界的王者，斜口鉗中的斜口鉗，單刃又薄刃，剪完少白痕少挖肉，但依然脆弱",
                    //        CostJP = 4800,
                    //        CostExchangeRate = 0.28f,
                    //        PriceExchangeRage = 0.35f,
                    //        Inventory = 15,
                    //        OrderedQuantity = 7,
                    //        ProductTypeID = "T",
                    //        ProductSpecificationID = "Cut",
                    //        BrandID = "13",
                    //        SupplierID = "02",
                    //        Photo = "PIC_TGOD_003.jpg"
                    //    },
                    //    //斜口鉗就這三把，垃圾就不要了
                    //    new Product
                    //    {
                    //        ProductID = "TOFLA004",
                    //        ProductName = "OFLA 筆刀",
                    //        Description = "OFLA的筆刀，做模型不打磨的神器",
                    //        CostJP = 200,
                    //        CostExchangeRate = 0.8f,
                    //        PriceExchangeRage = 1f,
                    //        Inventory = 15,
                    //        OrderedQuantity = 7,
                    //        ProductTypeID = "T",
                    //        ProductSpecificationID = "Blade",
                    //        BrandID = "14",
                    //        SupplierID = "07",
                    //        Photo = "PIC_TOFLA004.jpg"
                    //    },
                    //    new Product
                    //    {
                    //        ProductID = "COFLA001",
                    //        ProductName = "OFLA 筆刀刀片，刀柄另購",
                    //        Description = "OFLA的筆刀刀路，做模型不打磨的神器",
                    //        CostJP = 90,
                    //        CostExchangeRate = 0.8f,
                    //        PriceExchangeRage = 1f,
                    //        Inventory = 15,
                    //        OrderedQuantity = 7,
                    //        ProductTypeID = "C",
                    //        ProductSpecificationID = "Blade",
                    //        BrandID = "14",
                    //        SupplierID = "07",
                    //        Photo = "PIC_COFLA001.jpg"
                    //    },
                    //    new Product
                    //    {
                    //        ProductID = "TTAB_005",
                    //        ProductName = "TAMIYA 田宮 74040 模型用高級筆刀 45度",
                    //        Description = "田宮出品高級筆刀",
                    //        CostJP = 1210,
                    //        CostExchangeRate = 0.2f,
                    //        PriceExchangeRage = 0.24f,
                    //        Inventory = 9,
                    //        OrderedQuantity = 3,
                    //        ProductTypeID = "T",
                    //        ProductSpecificationID = "Blade",
                    //        BrandID = "06",
                    //        SupplierID = "02",
                    //        Photo = "PIC_TTAB_005.jpg"
                    //    },
                    //    new Product
                    //    {
                    //        ProductID = "TTAC_006",
                    //        ProductName = "Tamiya 田宮 蝕刻片專用夾鉗",
                    //        Description = "田宮出品剪蝕刻片專用",
                    //        CostJP = 2970,
                    //        CostExchangeRate = 0.18f,
                    //        PriceExchangeRage = 0.2f,
                    //        Inventory = 9,
                    //        OrderedQuantity = 3,
                    //        ProductTypeID = "T",
                    //        ProductSpecificationID = "Cut",
                    //        BrandID = "06",
                    //        SupplierID = "02",
                    //        Photo = "PIC_TTAC_006.jpg"
                    //    },
                    //    new Product
                    //    {
                    //        ProductID = "CTWXF028",
                    //        ProductName = "TAMIYA 田宮 大水性漆 23ml XF-28 消光暗銅色",
                    //        Description = "田宮水性漆-消光暗銅色",
                    //        CostJP = 264,
                    //        CostExchangeRate = 0.18f,
                    //        PriceExchangeRage = 0.2f,
                    //        Inventory = 9,
                    //        OrderedQuantity = 3,
                    //        ProductTypeID = "C",
                    //        ProductSpecificationID = "WaterPaint",
                    //        BrandID = "06",
                    //        SupplierID = "02",
                    //        Photo = "PIC_CTWXF028.jpg"
                    //    },
                    //    new Product
                    //    {
                    //        ProductID = "CTWXF016",
                    //        ProductName = "TAMIYA 田宮 大水性漆 23ml XF-16 消光銀色",
                    //        Description = "田宮水性漆-消光銀色",
                    //        CostJP = 220,
                    //        CostExchangeRate = 0.18f,
                    //        PriceExchangeRage = 0.2f,
                    //        Inventory = 9,
                    //        OrderedQuantity = 3,
                    //        ProductTypeID = "C",
                    //        ProductSpecificationID = "WaterPaint",
                    //        BrandID = "06",
                    //        SupplierID = "02",
                    //        Photo = "PIC_CTWXF016.jpg"
                    //    },
                    //    new Product
                    //    {
                    //        ProductID = "CTWXF023",
                    //        ProductName = "TAMIYA 田宮 大水性漆 23ml X-11 亮光銀色",
                    //        Description = "田宮水性漆-亮光銀色",
                    //        CostJP = 220,
                    //        CostExchangeRate = 0.18f,
                    //        PriceExchangeRage = 0.2f,
                    //        Inventory = 9,
                    //        OrderedQuantity = 3,
                    //        ProductTypeID = "C",
                    //        ProductSpecificationID = "WaterPaint",
                    //        BrandID = "06",
                    //        SupplierID = "02",
                    //        Photo = "PIC_CTWXF023.jpg"
                    //    },
                    //    new Product
                    //    {
                    //        ProductID = "CTWX_007",
                    //        ProductName = "TAMIYA 田宮 大水性漆 23ml X-7 亮光紅色",
                    //        Description = "田宮水性漆-亮光紅色",
                    //        CostJP = 220,
                    //        CostExchangeRate = 0.18f,
                    //        PriceExchangeRage = 0.2f,
                    //        Inventory = 9,
                    //        OrderedQuantity = 3,
                    //        ProductTypeID = "C",
                    //        ProductSpecificationID = "WaterPaint",
                    //        BrandID = "06",
                    //        SupplierID = "02",
                    //        Photo = "PIC_CTWX_007.jpg"
                    //    },
                    //    new Product
                    //    {
                    //        ProductID = "CTWX_003",
                    //        ProductName = "TAMIYA 田宮 大水性漆 23ml X-3 亮光深藍色",
                    //        Description = "田宮水性漆-亮光深藍色",
                    //        CostJP = 220,
                    //        CostExchangeRate = 0.18f,
                    //        PriceExchangeRage = 0.2f,
                    //        Inventory = 9,
                    //        OrderedQuantity = 3,
                    //        ProductTypeID = "C",
                    //        ProductSpecificationID = "WaterPaint",
                    //        BrandID = "06",
                    //        SupplierID = "02",
                    //        Photo = "PIC_CTWX_003.jpg"
                    //    },
                    //    new Product
                    //    {
                    //        ProductID = "CTWX_018",
                    //        ProductName = "TAMIYA 田宮 水性壓克力漆 X-18 半光黑色",
                    //        Description = "田宮水性漆-半光黑色",
                    //        CostJP = 220,
                    //        CostExchangeRate = 0.18f,
                    //        PriceExchangeRage = 0.2f,
                    //        Inventory = 9,
                    //        OrderedQuantity = 3,
                    //        ProductTypeID = "C",
                    //        ProductSpecificationID = "WaterPaint",
                    //        BrandID = "06",
                    //        SupplierID = "02",
                    //        Photo = "PIC_CTWX_018.jpg"
                    //    },
                    //    new Product
                    //    {
                    //        ProductID = "CTWX_025",
                    //        ProductName = "TAMIYA 田宮 水性壓克力漆 X-25 亮光透明綠色",
                    //        Description = "田宮水性漆-亮光透明綠色",
                    //        CostJP = 220,
                    //        CostExchangeRate = 0.18f,
                    //        PriceExchangeRage = 0.2f,
                    //        Inventory = 9,
                    //        OrderedQuantity = 3,
                    //        ProductTypeID = "C",
                    //        ProductSpecificationID = "WaterPaint",
                    //        BrandID = "06",
                    //        SupplierID = "02",
                    //        Photo = "PIC_CTWX_025.jpeg"
                    //    },
                    //    new Product
                    //    {
                    //        ProductID = "CTWX_027",
                    //        ProductName = "TAMIYA 田宮 水性壓克力漆 X-27 亮光透明紅色",
                    //        Description = "田宮水性漆-亮光透明紅色",
                    //        CostJP = 220,
                    //        CostExchangeRate = 0.18f,
                    //        PriceExchangeRage = 0.2f,
                    //        Inventory = 9,
                    //        OrderedQuantity = 3,
                    //        ProductTypeID = "C",
                    //        ProductSpecificationID = "WaterPaint",
                    //        BrandID = "06",
                    //        SupplierID = "02",
                    //        Photo = "PIC_CTWX_027.jpeg"
                    //    },
                    //    new Product
                    //    {
                    //        ProductID = "CTATS021",
                    //        ProductName = "TAMIYA TS-21 GOLD 有光金色 塑膠模型噴漆",
                    //        Description = "田宮水性漆-消光暗銅色",
                    //        CostJP = 770,
                    //        CostExchangeRate = 0.18f,
                    //        PriceExchangeRage = 0.2f,
                    //        Inventory = 9,
                    //        OrderedQuantity = 3,
                    //        ProductTypeID = "C",
                    //        ProductSpecificationID = "EnamelPaint",
                    //        BrandID = "06",
                    //        SupplierID = "02",
                    //        Photo = "PIC_CTATS021.jpg"
                    //    },
                    //    new Product
                    //    {
                    //        ProductID = "CTAXF002",
                    //        ProductName = "TAMIYA 水性漆(壓克力漆)XF-2 消光白色",
                    //        Description = "田宮水性漆-消光白色",
                    //        CostJP = 220,
                    //        CostExchangeRate = 0.18f,
                    //        PriceExchangeRage = 0.2f,
                    //        Inventory = 9,
                    //        OrderedQuantity = 3,
                    //        ProductTypeID = "C",
                    //        ProductSpecificationID = "WaterPaint",
                    //        BrandID = "06",
                    //        SupplierID = "02",
                    //        Photo = "PIC_CTATS021.jpg"
                    //    }

                    //    );
                    //_context.SaveChanges();

                    //_context.Member.AddRange(
                    //    new Member
                    //    {
                    //        MemberID = "M00001",
                    //        Name = "呼溜肯",
                    //        Address = "台南市中西區成功路1號",
                    //        Email = "aaa@gmail.com",
                    //        Point = 0
                    //    },
                    //    new Member
                    //    {
                    //        MemberID = "M00002",
                    //        Name = "春麗",
                    //        Address = "台南市中西區成功路2號",
                    //        Email = "bbb@gmail.com",
                    //        Point = 0
                    //    });
                    //_context.SaveChanges();

                    //_context.MemberTel.AddRange(
                    //    new MemberTel
                    //    {
                    //        MemberID = "M00001",
                    //        TelNumber = "0988123456"
                    //    },
                    //    new MemberTel
                    //    {
                    //        MemberID = "M00001",
                    //        TelNumber = "0987017987"
                    //    },
                    //    new MemberTel
                    //    {
                    //        MemberID = "M00002",
                    //        TelNumber = "0987543987"
                    //    });
                    //_context.SaveChanges();

                    //_context.MemberAcc.AddRange(
                    //    new MemberAcc
                    //    {
                    //        Account = "model001",
                    //        Mima = "123Model",
                    //        MemberID = "M00001"
                    //    },
                    //    new MemberAcc
                    //    {
                    //        Account = "model002",
                    //        Mima = "456Model",
                    //        MemberID = "M00002"
                    //    });
                    //_context.SaveChanges();


                    //_context.PayWay.AddRange(
                    //    new PayWay
                    //    {
                    //        PayWayID = "0",
                    //        PayWayName = "來店取貨付款"
                    //    },
                    //    new PayWay
                    //    {
                    //        PayWayID = "1",
                    //        PayWayName = "店到店取貨付款"
                    //    });
                    //_context.SaveChanges();

                    //_context.Ordertatus.AddRange(
                    //    new Ordertatus
                    //    {
                    //        OrdertatusID = "01",
                    //        OrdertatusName = "訂單已受理"
                    //    },
                    //    new Ordertatus
                    //    {
                    //        OrdertatusID = "02",
                    //        OrdertatusName = "訂單已處理"
                    //    },
                    //    new Ordertatus
                    //    {
                    //        OrdertatusID = "03",
                    //        OrdertatusName = "出貨中"
                    //    },
                    //    new Ordertatus
                    //    {
                    //        OrdertatusID = "04",
                    //        OrdertatusName = "已出貨"
                    //    },
                    //    new Ordertatus
                    //    {
                    //        OrdertatusID = "05",
                    //        OrdertatusName = "訂單完成"
                    //    },
                    //    new Ordertatus
                    //    {
                    //        OrdertatusID = "10",
                    //        OrdertatusName = "訂單已取消"
                    //    },
                    //    new Ordertatus
                    //    {
                    //        OrdertatusID = "11",
                    //        OrdertatusName = "廠商砍單"
                    //    },
                    //    new Ordertatus
                    //    {
                    //        OrdertatusID = "12",
                    //        OrdertatusName = "廠商延期"
                    //    });
                    //_context.SaveChanges();

                    //_context.Role.AddRange(
                    //    new Role
                    //    {
                    //        RoleID = "0",
                    //        RoleName = "系統管理員"
                    //    },
                    //    new Role
                    //    {
                    //        RoleID = "1",
                    //        RoleName = "店員"
                    //    });
                    //_context.SaveChanges();

                    //_context.ShippingWay.AddRange(
                    //    new ShippingWay
                    //    {
                    //        ShippingWayID = "1",
                    //        ShippingWayName = "自取"
                    //    },
                    //    new ShippingWay
                    //    {
                    //        ShippingWayID = "2",
                    //        ShippingWayName = "711取貨"
                    //    },
                    //    new ShippingWay
                    //    {
                    //        ShippingWayID = "3",
                    //        ShippingWayName = "全家取貨"
                    //    },
                    //    new ShippingWay
                    //    {
                    //        ShippingWayID = "4",
                    //        ShippingWayName = "萊爾富取貨"
                    //    },
                    //    new ShippingWay
                    //    {
                    //        ShippingWayID = "5",
                    //        ShippingWayName = "OK取貨"
                    //    });
                    //_context.SaveChanges();

                    //_context.Staff.AddRange(
                    //    new Staff
                    //    {
                    //        StaffID = "114001",
                    //        Name = "王大明",
                    //        ArrivalDate = new DateTime(2023, 02, 01),
                    //        Phone = "0912365478",
                    //        RoleID = "1"
                    //    }
                    //    );
                    //_context.SaveChanges();

                    //_context.Order.AddRange(
                    //    new Order
                    //    {
                    //        OrderNo = "N202412200001",
                    //        OrderDate = new DateTime(2024, 12, 20),
                    //        ShippingDate = null,
                    //        IsGoodPackage = false,
                    //        ShippingAddr = "台南市北區公園南路71號",
                    //        PayWayID = "0",
                    //        OrdertatusID = "01",
                    //        MemberID = "M00001",
                    //        ShippingWayID = "1"
                    //    }, new Order
                    //    {
                    //        OrderNo = "N202501090001",
                    //        OrderDate = DateTime.Now,
                    //        ShippingDate = null,
                    //        IsGoodPackage = true,
                    //        ShippingAddr = "台南市東區莊敬路90號",
                    //        PayWayID = "1",
                    //        OrdertatusID = "02",
                    //        MemberID = "M00001",
                    //        ShippingWayID = "2"
                    //    });
                    //_context.SaveChanges();

                    //_context.Invoice.AddRange(
                    //    new Invoice
                    //    {
                    //        OrderNo = "N202412200001",
                    //        InvoiceNo = "28630525"
                    //    });
                    //_context.SaveChanges();

                    //_context.HandleOrder.AddRange(
                    //    new HandleOrder
                    //    {
                    //        OrderNo = "N202412200001",
                    //        HandleTime = DateTime.Now,
                    //        StaffID = "114001"
                    //    },
                    //    new HandleOrder
                    //    {
                    //        OrderNo = "N202501090001",
                    //        HandleTime = new DateTime(2025, 1, 16),
                    //        StaffID = "114001"
                    //    });
                    //_context.SaveChanges();

                    //_context.OrderDetail.AddRange(
                    //    new OrderDetail
                    //    {
                    //        OrderNo = "N202412200001",
                    //        ProductID = "PMGEX002",
                    //        Off = 0f,
                    //        Vol = 1
                    //    },
                    //    new OrderDetail
                    //    {
                    //        OrderNo = "N202412200001",
                    //        ProductID = "PMGEX001",
                    //        Off = 0f,
                    //        Vol = 1
                    //    },
                    //    new OrderDetail
                    //    {
                    //        OrderNo = "N202501090001",
                    //        ProductID = "PMGSD001",
                    //        Off = 0f,
                    //        Vol = 1
                    //    },
                    //    new OrderDetail
                    //    {
                    //        OrderNo = "N202501090001",
                    //        ProductID = "PMGSD002",
                    //        Off = 0f,
                    //        Vol = 1
                    //    },
                    //    new OrderDetail
                    //    {
                    //        OrderNo = "N202501090001",
                    //        ProductID = "PMGSD003",
                    //        Off = 0f,
                    //        Vol = 1
                    //    });
                    //_context.SaveChanges();


                }
            }
        }
    }
}
