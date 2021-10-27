using SaladProject.Models;
using SaladProject.Util;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace SaladProject.Services
{
    public static class GameService
    {
        private static JsonElement DeserializeJson(string json)
        {
            JsonDocument doc = JsonDocument.Parse(json);
            return doc.RootElement;
        }

        private static Game CondenseGame(JsonElement root)
        {
            if
            (
                !(
                    root.TryGetProperty("id", out JsonElement gameIdJson) &&
                    root.TryGetProperty("name", out JsonElement nameJson) &&
                    root.TryGetProperty("added", out JsonElement addedJson) &&
                    root.TryGetProperty("metacritic", out JsonElement metacriticJson) &&
                    root.TryGetProperty("rating", out JsonElement ratingJson) &&
                    root.TryGetProperty("released", out JsonElement releasedJson) &&
                    root.TryGetProperty("updated", out JsonElement updatedJson)
                )
            )
            {
                File.WriteAllText(Path.Combine(Environment.CurrentDirectory, "log.txt"), "null1");
                return null;
            }

            bool noMetacritic = false;

            if (metacriticJson.ValueKind == JsonValueKind.Null)
            {
                noMetacritic = true;
            }

            if
            (
                !(
                    gameIdJson.TryGetInt32(out int gameId)                    
                )
            )
            {
                File.WriteAllText(Path.Combine(Environment.CurrentDirectory, "log.txt"), "null2");
                return null;
            }

            if
            (
                !(
                    addedJson.TryGetInt32(out int added)
                )
            )
            {
                File.WriteAllText(Path.Combine(Environment.CurrentDirectory, "log.txt"), "null3");
                return null;
            }

            int metacritic = -1;
            if (!noMetacritic)
            {
                if
                (
                    !(
                        metacriticJson.TryGetInt32(out metacritic)
                    )
                )
                {
                    File.WriteAllText(Path.Combine(Environment.CurrentDirectory, "log.txt"), "null4");
                    return null;
                }
            }

            if
            (
                !(
                    ratingJson.TryGetDouble(out double rating)
                )
            )
            {
                File.WriteAllText(Path.Combine(Environment.CurrentDirectory, "log.txt"), "null5");
                return null;
            }

            string name = nameJson.GetString();
            string released = releasedJson.GetString();
            string updated = updatedJson.GetString();
            
            return new Game
            {
                GameId = gameId,
                Name = name,
                Added = added,
                Metacritic = metacritic,
                Rating = rating,
                Released = released,
                Updated = updated
            };
        }

        private static Game ConvertGame(string jsonGame)
        {
            JsonElement root = DeserializeJson(jsonGame);
            return CondenseGame(root);
        }

        private static List<Game> ConvertGameList(string json)
        {
            JsonElement root = DeserializeJson(json);
            if (!root.TryGetProperty("results", out JsonElement jsonList))
            {
                return null;
            }
            List<Game> lst = new List<Game>();
            foreach (JsonElement gameJson in jsonList.EnumerateArray())
            {
                lst.Add(CondenseGame(gameJson));
            }
            return lst;
        }

        public static async Task<Game> GetGame(int gameId)
        {
            string json = await HttpUtil.QueryGame(gameId.ToString());
            return ConvertGame(json);
        }

        public static async Task<List<Game>> GetGames(string q, string sort) {
            string json = await HttpUtil.QueryGames(q, sort);
            return ConvertGameList(json);
        }
    }
}