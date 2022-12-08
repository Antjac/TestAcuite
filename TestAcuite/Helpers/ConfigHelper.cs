﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TestAcuite.Class;

namespace TestAcuite.Helpers
{
    public class ConfigHelper
    {
        const string PARAM_FILENAME = "calibration.json";

        public static void SaveCalibration(CalibrationParams p)
        {
            string cacheDir = FileSystem.Current.CacheDirectory;
            string jsonString = JsonSerializer.Serialize(p);
            File.WriteAllText(Path.Combine(cacheDir, PARAM_FILENAME), jsonString);
        }

        public static CalibrationParams GetCalibration()
        {
            string cacheDir = FileSystem.Current.CacheDirectory;
            if (File.Exists(Path.Combine(cacheDir, PARAM_FILENAME)))
            {
                string paramsString = File.ReadAllText(Path.Combine(cacheDir, PARAM_FILENAME));
                CalibrationParams newParams = JsonSerializer.Deserialize<CalibrationParams>(paramsString);
                return newParams;

            }
            else
            {
                return null;
            }

        }

    }
}
