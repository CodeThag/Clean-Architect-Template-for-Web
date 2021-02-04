using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class GlobalSetting : IGlobalSetting
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<GlobalSetting> _logger;

        public GlobalSetting(IApplicationDbContext context, ILogger<GlobalSetting> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> GetBooleanValue(string Key)
        {
            Setting setting = await _context.Settings.FirstOrDefaultAsync(x => x.Name.ToUpper() == Key.ToUpper());

            if (null == setting)
                throw new Exception("Setting not found!");

            if ("Bool" != setting.Type)
                throw new Exception("Data type mismatch!");

            bool value = false;
            if (!bool.TryParse(setting.Value, out value))
            {
                throw new Exception("Value is not of data type bool!");
            }

            return value;

        }

        public async Task<DateTime> GetDateTimeValue(string Key)
        {
            Setting setting = await _context.Settings.FirstOrDefaultAsync(x => x.Name.ToUpper() == Key.ToUpper());

            if (null == setting)
                throw new Exception("Setting not found!");

            if ("DateTime" != setting.Type)
                throw new Exception("Data type mismatch!");

            DateTime value = new DateTime();
            if (!DateTime.TryParse(setting.Value, out value))
            {
                throw new Exception("Value is not of data type DateTime!");
            }

            return value;
        }

        public async Task<Decimal> GetDecimalValue(string Key)
        {
            Setting setting = await _context.Settings.FirstOrDefaultAsync(x => x.Name.ToUpper() == Key.ToUpper());

            if (null == setting)
                throw new Exception("Setting not found!");

            if ("Decimal" != setting.Type)
                throw new Exception("Data type mismatch!");

            Decimal value = new Decimal();
            if (!Decimal.TryParse(setting.Value, out value))
            {
                throw new Exception("Value is not of data type Decimal!");
            }

            return value;

        }

        public async Task<Double> GetDoubleValue(string Key)
        {

            Setting setting = await _context.Settings.FirstOrDefaultAsync(x => x.Name.ToUpper() == Key.ToUpper());

            if (null == setting)
                throw new Exception("Setting not found!");

            if ("Double" != setting.Type)
                throw new Exception("Data type mismatch!");

            Double value = 0;
            if (!Double.TryParse(setting.Value, out value))
            {
                throw new Exception("Value is not of data type Double!");
            }

            return value;
        }

        public async Task<float> GetFloatValue(string Key)
        {
            Setting setting = await _context.Settings.FirstOrDefaultAsync(x => x.Name.ToUpper() == Key.ToUpper());

            if (null == setting)
                throw new Exception("Setting not found!");

            if ("Float" != setting.Type)
                throw new Exception("Data type mismatch!");

            float value = 0;
            if (!float.TryParse(setting.Value, out value))
            {
                throw new Exception("Value is not of data type float!");
            }

            return value;

        }

        public async Task<int> GetIntValue(string Key)
        {
            Setting setting = await _context.Settings.FirstOrDefaultAsync(x => x.Name.ToUpper() == Key.ToUpper());

            if (null == setting)
                throw new Exception("Setting not found!");

            if ("Int" != setting.Type)
                throw new Exception("Data type mismatch");

            int value = 0;
            if (!Int32.TryParse(setting.Value, out value))
            {
                throw new Exception("Value is not of data type int");
            }

            return value;
        }

        public async Task<long> GetLongValue(string Key)
        {
            Setting setting = await _context.Settings.FirstOrDefaultAsync(x => x.Name.ToUpper() == Key.ToUpper());

            if (null == setting)
                throw new Exception("Setting not found!");

            if ("Long" != setting.Type)
                throw new Exception("Data type mismatch!");

            long value = 0;
            if (!long.TryParse(setting.Value, out value))
            {
                throw new Exception("Value is not of data type Long!");
            }

            return value;
        }

        public async Task<string> GetStringValue(string Key)
        {
            Setting setting = await _context.Settings.FirstOrDefaultAsync(x => x.Name.ToUpper() == Key.ToUpper());

            if (null == setting)
                throw new Exception("Setting not found!");

            if ("String" != setting.Type)
                throw new Exception("Data type mismatch!");

            return setting.Value;
        } 
    }
}
