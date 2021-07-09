using HBCampaignModule.Domain.Model;
using HBCampaignModule.Domain.Repositories;
using HBCampaignModule.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBCampaignModule.Application.Repositories
{
    public class IncreaseTimeRepository : IIncreaseTimeRepository<Time>
    {
        public IncreaseTimeRepository()
        {
            var fileFullPath = Path.Combine(StaticValue.IncreaseTimeFile, StaticValue.IncreaseTimeFileName);

            if (!Directory.Exists(StaticValue.IncreaseTimeFile))
                Directory.CreateDirectory(StaticValue.IncreaseTimeFile);

            if (!File.Exists(fileFullPath))
            {
                FileStream file = File.Create(fileFullPath);
                file.Close();
            }
        }
        public async Task<string> CreateAsync(Time entity)
        {
            if (entity.Hour > 0)
            {
                var time = await ReadData();
                if (time.Hour == 0)
                {
                    if (await WritaData(entity))
                        return string.Format(StaticValue.IncreaseTimeSuccessMessage, entity.Hour);
                    else
                        return StaticValue.TimeAdvanceFailed;
                }
                else
                {
                    if (await UpdateAsync(entity))
                        return string.Format(StaticValue.IncreaseTimeSuccessMessage, (time.Hour + entity.Hour));
                    else
                        return StaticValue.TimeAdvanceFailed;
                }
            }
            else
                return StaticValue.TimeAdvanceFailed;
        }

        public async Task<int> GetTimeAsync()
        {
            var time = await ReadData();
            return time.Hour;
        }

        public async Task<Time> ReadData()
        {
            Time time = new Time();
            try
            {
                var fileFullPath = Path.Combine(StaticValue.IncreaseTimeFile, StaticValue.IncreaseTimeFileName);
                string[] readResult = File.ReadAllLines(fileFullPath);
                if (readResult.Length > 0)
                    time.Hour = Convert.ToInt32(readResult[0]);
            }
            catch (Exception ex)
            {
                throw;
            }
            return time;
        }

        public async Task<bool> UpdateAsync(Time entity)
        {
            var time = await ReadData();
            time.Hour += entity.Hour;
            return await UpdateData(time);
        }

        public async Task<bool> UpdateData(Time entity)
        {
            var status = false;
            try
            {
                var fileFullPath = Path.Combine(StaticValue.IncreaseTimeFile, StaticValue.IncreaseTimeFileName);
                if (File.Exists(fileFullPath))
                    File.Delete(fileFullPath);

                if (await WritaData(entity))
                    status = true;
                else
                    status = false;
            }
            catch (Exception)
            {
                throw;
            }
            return status;
        }

        public async Task<bool> WritaData(Time entity)
        {
            var status = false;
            try
            {
                var fileFullPath = Path.Combine(StaticValue.IncreaseTimeFile, StaticValue.IncreaseTimeFileName);
                if (!Directory.Exists(StaticValue.IncreaseTimeFile))
                    Directory.CreateDirectory(StaticValue.IncreaseTimeFile);

                if (!File.Exists(fileFullPath))
                {
                    FileStream file = File.Create(fileFullPath);
                    file.Close();
                }

                FileStream fs = new FileStream(fileFullPath, FileMode.Append, FileAccess.Write, FileShare.Write);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(entity.Hour);
                sw.Close();
                status = true;
            }
            catch (Exception)
            {
                throw;
            }
            return status;
        }
    }
}
