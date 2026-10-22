using System;
using System.Collections.Generic;

namespace DSA.Lab00
{
    public sealed class PlayerCollection
    {
        private readonly List<Player> _players;

        public PlayerCollection(IEnumerable<Player> players)
        {
            if (players == null)
            {
                throw new ArgumentNullException(nameof(players));
            }

            _players = new List<Player>(players);
        }

        public Player? FindById(int id)
        {
            foreach (Player player in _players)
            {
                if (player.Id == id)
                {
                    return player;
                }
            }

            return null;
        }

        public Player? FindHighestScore()
        {
            Player? best = null;

            foreach (Player player in _players)
            {
                if (best == null || player.Score > best.Score)
                {
                    best = player;
                }
            }
            return best;
        }

        public bool ContainsName(string name)
        {

           foreach (Player player in _players)
            {
                if (player.Name == name)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
