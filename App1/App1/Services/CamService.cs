using App1.Models;

using DynamicData.Kernel;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace App1.Services
{
    public static class CamService
    {
        public static async Task<bool> SaveStreamToJsonAsync(string filePath, VideoModel viewModel, Stream cameraStream)
        {
            return await Task.Run(async () =>
            {
                try
                {
                    using (var fileStream = File.OpenWrite(filePath))
                    {

                        JsonWriterOptions writerOptions = new JsonWriterOptions { Indented = true, };
                        using (Utf8JsonWriter writer = new Utf8JsonWriter(fileStream, writerOptions))
                        {

                            writer.WriteStartObject();
                            writer.WriteString("api_key", viewModel.api_key);
                            writer.WriteString("geopos", viewModel.geopos);

                            writer.WriteStartArray("video");

                            while (cameraStream.ReadByte() != -1)
                            {

                                writer.WriteRawValue(cameraStream.ReadByte().ToString(), true);
                            }

                            writer.WriteEndArray();
                            writer.WriteString("video_suf", viewModel.video_suf);
                            writer.WriteString("room_type", viewModel.room_type);
                            writer.WriteString("floor", viewModel.floor);
                            writer.WriteString("flat", viewModel.flat);

                            writer.WriteEndObject();
                            writer.Flush();
                        }
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                
              

            });
        }
    }
}
