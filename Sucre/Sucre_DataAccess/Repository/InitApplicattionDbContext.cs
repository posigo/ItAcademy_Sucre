using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Sucre_DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sucre_DataAccess.Data;
using Microsoft.Extensions.Configuration;

namespace Sucre_DataAccess.Repository
{
    public class InitApplicattionDbContext
    {
        private ApplicationDbContext _db;
        private IConfiguration _configuration;
        
        private List<ParameterType> _parameterTypes;
        private List<Energy> _energies;
        private List<Cex> _cexs;
        private List<Point> _points;
        private List<Canal> _cannales;
        private List<AsPaz> _asPazs;

        public InitApplicattionDbContext(ApplicationDbContext db, IConfiguration configuration
                                        )
        {
            _db = db;
            _configuration = configuration;
        }

        public string DatabaseName
        {
            get
            {

                //var connstr = _configuration.GetConnectionString("DefaultConnection").ToString();
                //string strDatabase = connstr.Split(';').ToList().FirstOrDefault(item => item.Contains("Database"));
                //string result = strDatabase.Split('=').Last().ToString();
                return _configuration.GetConnectionString("DefaultConnection").ToString().
                            Split(';').ToList().FirstOrDefault(item => item.Contains("Database")).
                                Split('=').Last().ToString();

            }
            
        }

        private void SetValueDb()
        {            
            _parameterTypes = new List<ParameterType>()
            {
                new ParameterType()
                {
                    Name = "Давление",
                    Mnemo = "P",
                    UnitMeas = "МПа"
                },
                new ParameterType()
                {
                    Name = "Давление",
                    Mnemo = "P",
                    UnitMeas = "кПа"
                },new ParameterType()
                {
                    Name = "Давление",
                    Mnemo = "P",
                    UnitMeas = "bar"
                },new ParameterType()
                {
                    Name = "Температура",
                    Mnemo = "T",
                    UnitMeas = "°C"
                },
                new ParameterType()
                {
                    Name = "Температура",
                    Mnemo = "T",
                    UnitMeas = "K"
                },new ParameterType()
                {
                    Name = "Расход в м3",
                    Mnemo = "Q",
                    UnitMeas = "м3"
                },new ParameterType()
                {
                    Name = "Расход в тоннах",
                    Mnemo = "Q",
                    UnitMeas = "т"
                }
            };
            _db.ParameterTypes.AddRange(_parameterTypes);            
            _energies = new List<Energy>()
            {
                new Energy()
                {
                    EnergyName = "Газ"
                },
                new Energy()
                {
                    EnergyName = "Вода"
                },
                new Energy()
                {
                    EnergyName = "Пар"
                },
                new Energy()
                {
                    EnergyName = "Азот"
                },
                new Energy()
                {
                    EnergyName = "Сжатый воздух"
                }
            };
            _db.Energies.AddRange(_energies);
            _cexs = new List<Cex>()
            {
                new Cex()
                {
                    CexName = "КОЦ и ЭГУ",
                    Device = "Установка генераторов"
                },
                new Cex()
                {
                    Management = "ОГЭ",
                    Device = "УООС"
                },
                new Cex()
                {
                    CexName = "ОВиТ",
                    Device = "БРВ"
                },
                new Cex()
                {
                    Management = "УАМИТ",
                    CexName = "ЦЭОПЕК",
                    Location = "Сан Узел №5"
                },
                new Cex()
                {
                    CexName = "КИПиА",
                    Area = "уч электронной лабаратории"
                }
            };
            _db.Cexs.AddRange(_cexs);
            _db.SaveChanges();
            _points = new List<Point>()
            {
                new Point()
                {
                    Name = "Учёт газа ЭГУ1",
                    Description = "Учёт расхода газа на электрогенераторной установке. Комерческий!",
                    EnergyId = _energies.FirstOrDefault(item => item.EnergyName == "Газ").Id,
                    Energy = _energies.FirstOrDefault(item => item.EnergyName == "Газ"),
                    CexId = _cexs.FirstOrDefault(item => item.CexName == "КОЦ и ЭГУ").Id,
                    Cex = _cexs.FirstOrDefault(item => item.CexName == "КОЦ и ЭГУ"),
                    ServiceStaff = "Цех КИПиА"
                },
                new Point()
                {
                    Name = "Учёт газа ЭГУ2",
                    EnergyId = _energies.FirstOrDefault(item => item.EnergyName == "Газ").Id,
                    Energy = _energies.FirstOrDefault(item => item.EnergyName == "Газ"),
                    CexId = _cexs.FirstOrDefault(item => item.CexName == "КОЦ и ЭГУ").Id,
                    Cex = _cexs.FirstOrDefault(item => item.CexName == "КОЦ и ЭГУ")

                },
                new Point()
                {
                    Name = "Учёт воды на ТП2",
                    EnergyId = _energies.FirstOrDefault(item => item.EnergyName == "Вода").Id,
                    Energy = _energies.FirstOrDefault(item => item.EnergyName == "Вода"),
                    CexId = _cexs.FirstOrDefault(item => item.CexName == "ОВиТ" && item.Device == "БРВ").Id,
                    Cex = _cexs.FirstOrDefault(item => item.CexName == "ОВиТ" && item.Device == "БРВ"),
                    ServiceStaff = "Служба ОВИТ"
                },
                new Point()
                {
                    Name = "Metering Test",
                    EnergyId = _energies.FirstOrDefault(item => item.EnergyName == "Сжатый воздух").Id,
                    Energy = _energies.FirstOrDefault(item => item.EnergyName == "Сжатый воздух"),
                    CexId = _cexs.FirstOrDefault(item => item.CexName == "КОЦ и ЭГУ").Id,
                    Cex = _cexs.FirstOrDefault(item => item.CexName == "КОЦ и ЭГУ")
                }
            };
            _db.Points.AddRange(_points);
            _cannales = new List<Canal>()
            {
                new Canal()
                {
                    Name = "Давление газа ЭГУ1",
                    Description = "TestCanalDescription",
                    ParameterTypeId = _parameterTypes.FirstOrDefault(item => item.Name == "Давление" && item.Mnemo == "P" && item.UnitMeas == "МПа").Id,
                    ParameterType = _parameterTypes.FirstOrDefault(item => item.Name == "Давление" && item.Mnemo == "P" && item.UnitMeas == "МПа"),
                    Reader = true,
                    SourceType = 0,
                    AsPazEin = true
                },
                new Canal()
                {
                    Name = "Давление воды ТП2",
                    Description = "TestCanalDescr2",
                    ParameterTypeId = _parameterTypes.FirstOrDefault(item => item.Name == "Давление" && item.Mnemo == "P" && item.UnitMeas == "кПа").Id,
                    ParameterType = _parameterTypes.FirstOrDefault(item => item.Name == "Давление" && item.Mnemo == "P" && item.UnitMeas == "кПа"),
                    Reader = true,
                    SourceType = 1,
                    AsPazEin = true
                },
                new Canal()
                {
                    Name = "Давление газа ЭГУ2",
                    Description = "Descr3",
                    ParameterTypeId = _parameterTypes.FirstOrDefault(item => item.Name == "Давление" && item.Mnemo == "P" && item.UnitMeas == "МПа").Id,
                    ParameterType = _parameterTypes.FirstOrDefault(item => item.Name == "Давление" && item.Mnemo == "P" && item.UnitMeas == "МПа"),
                    Reader = true,
                    SourceType = 0,
                    AsPazEin = true
                },
                new Canal()
                {
                    Name = "Температура газа на ЭГУ",
                    Description = "Температура газа на на общей магистрали ЭГУ",
                    ParameterTypeId = _parameterTypes.FirstOrDefault(item => item.Name == "Температура" && item.Mnemo == "T" && item.UnitMeas == "°C").Id,
                    ParameterType = _parameterTypes.FirstOrDefault(item => item.Name == "Температура" && item.Mnemo == "T" && item.UnitMeas == "°C"),
                    Reader = true,
                    SourceType = 0,
                    AsPazEin = true
                },
                new Canal()
                {
                    Name = "Температура воды на ТП2",
                    Description = "Температура воды на ТП2",
                    ParameterTypeId = _parameterTypes.FirstOrDefault(item => item.Name == "Температура" && item.Mnemo == "T" && item.UnitMeas == "°C").Id,
                    ParameterType = _parameterTypes.FirstOrDefault(item => item.Name == "Температура" && item.Mnemo == "T" && item.UnitMeas == "°C"),
                    Reader = true,
                    SourceType = 0,
                    AsPazEin = false
                },
                new Canal()
                {
                    Name = "Расход газа на ЭГУ1",
                    Description = "Расход газа на ЭГУ1",
                    ParameterTypeId = _parameterTypes.FirstOrDefault(item => item.Name == "Расход в м3" && item.Mnemo == "Q" && item.UnitMeas == "м3").Id,
                    ParameterType = _parameterTypes.FirstOrDefault(item => item.Name == "Расход в м3" && item.Mnemo == "Q" && item.UnitMeas == "м3"),
                    Reader = true,
                    SourceType = 0,
                    AsPazEin = false
                },
                new Canal()
                {
                    Name = "Расход газа на ЭГУ2",
                    Description = "Расход газа на ЭГУ2",
                    ParameterTypeId = _parameterTypes.FirstOrDefault(item => item.Name == "Расход в м3" && item.Mnemo == "Q" && item.UnitMeas == "м3").Id,
                    ParameterType = _parameterTypes.FirstOrDefault(item => item.Name == "Расход в м3" && item.Mnemo == "Q" && item.UnitMeas == "м3"),
                    Reader = true,
                    SourceType = 0,
                    AsPazEin = false
                },
                new Canal()
                {
                    Name = "Расход воды",
                    Description = "Расход воды на ТП2",
                    ParameterTypeId = _parameterTypes.FirstOrDefault(item => item.Name == "Расход в тоннах" && item.Mnemo == "Q" && item.UnitMeas == "т").Id,
                    ParameterType = _parameterTypes.FirstOrDefault(item => item.Name == "Расход в тоннах" && item.Mnemo == "Q" && item.UnitMeas == "т"),
                    Reader = true,
                    SourceType = 0,
                    AsPazEin = false
                }
            };            
            _db.Canals.AddRange(_cannales);
            _db.SaveChanges();
            _asPazs = new List<AsPaz>()
            {
                new AsPaz()
                {
                    High = 100,
                    Low = 0,
                    A_HighEin = true,
                    A_HighType = false,
                    A_High = 95,
                    W_HighEin = true,
                    W_HighType = false,
                    W_High = 90,
                    W_LowEin = true,
                    W_LowType = true,
                    W_Low = 10,
                    A_LowEin = true,
                    A_LowType = false,
                    A_Low = 5,
                    CanalId = _cannales.FirstOrDefault(item => item.Name == "Давление воды ТП2").Id
                },
                new AsPaz()
                {
                    High = 50,
                    Low = 5,
                    A_HighEin = true,
                    A_HighType = false,
                    A_High = 45,
                    W_HighEin = false,
                    W_HighType = false,
                    W_High = 0,
                    W_LowEin = false,
                    W_LowType = false,
                    W_Low = 0,
                    A_LowEin = true,
                    A_LowType = false,
                    A_Low = 10,
                    CanalId = _cannales.FirstOrDefault(item => item.Name == "Давление газа ЭГУ2").Id
                },
                new AsPaz()
                {
                    High = 200,
                    Low = -50,
                    A_HighEin = true,
                    A_HighType = false,
                    A_High = 180,
                    W_HighEin = false,
                    W_HighType = false,
                    W_High = 0,
                    W_LowEin = false,
                    W_LowType = false,
                    W_Low = 0,
                    A_LowEin = false,
                    A_LowType = false,
                    A_Low = 0,
                    CanalId = _cannales.FirstOrDefault(item => item.Name == "Температура газа на ЭГУ").Id
                }
            };
            _db.AsPazs.AddRange(_asPazs);
            _db.SaveChanges();
            foreach (var asPaz in _asPazs)
            {
                Canal canal = _cannales.FirstOrDefault(item => item.Id == asPaz.CanalId);
                _cannales.IndexOf(canal);
                _cannales[_cannales.IndexOf(canal)].AsPaz = asPaz;
                asPaz.Canal = canal;
            }
            _db.UpdateRange(_asPazs);
            foreach (var paramType in _parameterTypes)
            {
                ICollection<Canal> cans = new HashSet<Canal>();
                cans = _cannales.Where(item => item.ParameterType == paramType).ToList();
                paramType.Canals = new HashSet<Canal>();
                foreach (var can in cans)
                {
                    paramType.Canals.Add(can);
                }
            }
            _db.ParameterTypes.UpdateRange(_parameterTypes);
            foreach (var energy in _energies)
            {
                ICollection<Point> pointss = new HashSet<Point>();
                pointss = _points.Where(item => item.Energy == energy).ToList();
                energy.Points = new HashSet<Point>();
                foreach (var ppoint in pointss)
                {
                    energy.Points.Add(ppoint);
                }
            }
            _db.Energies.UpdateRange(_energies);
            foreach (var cex in _cexs)
            {
                ICollection<Point> pointss = new HashSet<Point>();
                pointss = _points.Where(item => item.Cex == cex).ToList();
                cex.Points = new HashSet<Point>();
                foreach (var ppoint in pointss)
                {
                    cex.Points.Add(ppoint);
                }
            }
            _db.Cexs.UpdateRange(_cexs);
            _db.SaveChanges();
            //point 1
            Point pointCannales = _points.FirstOrDefault(item => item.Name == "Учёт газа ЭГУ1");
            Canal cannale = _cannales.FirstOrDefault(item => item.Name == "Давление газа ЭГУ1");
            pointCannales.Canals.Add(cannale);
            cannale = _cannales.FirstOrDefault(item => item.Name == "Температура газа на ЭГУ");
            pointCannales.Canals.Add(cannale);
            cannale = _cannales.FirstOrDefault(item => item.Name == "Расход газа на ЭГУ1");
            pointCannales.Canals.Add(cannale);
            cannale = _cannales.FirstOrDefault(item => item.Name == "Расход воды");
            pointCannales.Canals.Add(cannale);
            //point 2
            pointCannales = _points.FirstOrDefault(item => item.Name == "Учёт газа ЭГУ2");
            cannale = _cannales.FirstOrDefault(item => item.Name == "Давление газа ЭГУ2");
            pointCannales.Canals.Add(cannale);
            cannale = _cannales.FirstOrDefault(item => item.Name == "Температура газа на ЭГУ");
            pointCannales.Canals.Add(cannale);
            cannale = _cannales.FirstOrDefault(item => item.Name == "Расход газа на ЭГУ2");
            pointCannales.Canals.Add(cannale);
            //point 3
            pointCannales = _points.FirstOrDefault(item => item.Name == "Учёт воды на ТП2");
            cannale = _cannales.FirstOrDefault(item => item.Name == "Давление воды ТП2");
            pointCannales.Canals.Add(cannale);
            cannale = _cannales.FirstOrDefault(item => item.Name == "Температура воды на ТП2");
            pointCannales.Canals.Add(cannale);
            cannale = _cannales.FirstOrDefault(item => item.Name == "Расход воды");
            pointCannales.Canals.Add(cannale);
            //point 4
            pointCannales = _points.FirstOrDefault(item => item.Name == "Metering Test");
            cannale = _cannales.FirstOrDefault(item => item.Name == "Давление газа ЭГУ1");
            pointCannales.Canals.Add(cannale);
            cannale = _cannales.FirstOrDefault(item => item.Name == "Температура газа на ЭГУ");
            pointCannales.Canals.Add(cannale);
            cannale = _cannales.FirstOrDefault(item => item.Name == "Расход газа на ЭГУ2");
            pointCannales.Canals.Add(cannale);
            cannale = _cannales.FirstOrDefault(item => item.Name == "Расход воды");
            pointCannales.Canals.Add(cannale);
            _db.Points.UpdateRange(_points);
            _db.SaveChanges();
            //canale 1
            Canal cannalePoints = _cannales.FirstOrDefault(item => item.Name == "Давление газа ЭГУ1");
            Point point = _points.FirstOrDefault(item => item.Name == "Учёт газа ЭГУ1");
            cannalePoints.Points.Add(point);
            point = _points.FirstOrDefault(item => item.Name == "Metering Test");
            cannalePoints.Points.Add(point);
            //canale 2
            cannalePoints = _cannales.FirstOrDefault(item => item.Name == "Давление воды ТП2");
            point = _points.FirstOrDefault(item => item.Name == "Учёт воды на ТП2");
            cannalePoints.Points.Add(point);
            //canale 3
            cannalePoints = _cannales.FirstOrDefault(item => item.Name == "Давление газа ЭГУ2");
            point = _points.FirstOrDefault(item => item.Name == "Учёт газа ЭГУ2");
            cannalePoints.Points.Add(point);
            //canale 4
            cannalePoints = _cannales.FirstOrDefault(item => item.Name == "Температура газа на ЭГУ");
            point = _points.FirstOrDefault(item => item.Name == "Учёт газа ЭГУ1");
            cannalePoints.Points.Add(point);
            point = _points.FirstOrDefault(item => item.Name == "Учёт газа ЭГУ2");
            cannalePoints.Points.Add(point);
            point = _points.FirstOrDefault(item => item.Name == "Metering Test");
            cannalePoints.Points.Add(point);
            //canale 5
            cannalePoints = _cannales.FirstOrDefault(item => item.Name == "Температура воды на ТП2");
            point = _points.FirstOrDefault(item => item.Name == "Учёт воды на ТП2");
            cannalePoints.Points.Add(point);
            //canale 6
            cannalePoints = _cannales.FirstOrDefault(item => item.Name == "Расход газа на ЭГУ1");
            point = _points.FirstOrDefault(item => item.Name == "Учёт газа ЭГУ1");
            cannalePoints.Points.Add(point);
            //canale 7
            cannalePoints = _cannales.FirstOrDefault(item => item.Name == "Расход газа на ЭГУ2");
            point = _points.FirstOrDefault(item => item.Name == "Учёт газа ЭГУ2");
            cannalePoints.Points.Add(point);
            point = _points.FirstOrDefault(item => item.Name == "Metering Test");
            cannalePoints.Points.Add(point);
            //canale 8
            cannalePoints = _cannales.FirstOrDefault(item => item.Name == "Расход воды");
            point = _points.FirstOrDefault(item => item.Name == "Учёт газа ЭГУ1");
            cannalePoints.Points.Add(point);
            point = _points.FirstOrDefault(item => item.Name == "Учёт воды на ТП2");
            cannalePoints.Points.Add(point);
            point = _points.FirstOrDefault(item => item.Name == "Metering Test");
            cannalePoints.Points.Add(point);
            _db.Canals.UpdateRange(_cannales);

            _db.SaveChanges();
        }

        public bool InitDbValue(out string errMsg)
        {
            var sds = _db.ContextId;
            var ddd = _db.Database;
            try
            {
                _db.Database.EnsureDeleted();
                _db.Database.EnsureCreated();                
                SetValueDb();
                errMsg = "";
            }
            catch (Exception ex) 
            {
                errMsg = ex.Message.ToString();
                return false;
            }
            return true;
            
        }
    }
}
