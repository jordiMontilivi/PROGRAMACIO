using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassePokemon
{
	public class Pokemon
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public PokemonType Type { get; set; }
		public int Level { get; set; }
		public int Health { get; set; }
		public int Attack { get; set; }
		public int Defense { get; set; }
		public int Speed { get; set; }

		public Pokemon(int id, string name, PokemonType type, int level, int health, int attack, int defense, int speed)
		{
			Id = id;
			Name = name;
			Type = type;
			Level = level;
			Health = health;
			Attack = attack;
			Defense = defense;
			Speed = speed;
		}
		public override string ToString()
		{
			return $"Id: {Id}, Name: {Name}, Type: {Type}, Level: {Level}, Health: {Health}, Attack: {Attack}, Defense: {Defense}, Speed: {Speed}";
		}
		public override bool Equals(object obj)
		{
			if (obj is Pokemon other)
				return Id == other.Id;
			return false;
		}
		public override int GetHashCode()
		{
			//Podem indicar el hash code que volem, es bo fer-ho combinant les propietats que volem comparar
			return HashCode.Combine(Id, Name, Type, Level, Health, Attack, Defense, Speed);
		}
		// Definim l'enumerat dintre de la classe perque només te sentint per enumerar els tipus de Pokemon
		public enum PokemonType
		{
			Fire,
			Water,
			Grass,
			Electric,
			Ice,
			Fighting,
			Psychic,
			Rock,
			Ground,
			Fairy
		}
	}
}
