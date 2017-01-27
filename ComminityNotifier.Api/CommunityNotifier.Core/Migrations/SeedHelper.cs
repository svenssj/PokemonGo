﻿using System.Data.Entity.Migrations;
using CommunityNotifier.Core.Domain.Model;
using CommunityNotifier.Core.Domain.Repository;

namespace CommunityNotifier.Core.Migrations
{
    public class SeedHelper
    {

        public static void SeedAreas(SightingsContext context)
        {
            context.Areas.AddOrUpdate(a => a.AreaName,
 new Area() { AreaName = "Annat Område" },
 new Area() { AreaName = "Bjurhovda" },
 new Area() { AreaName = "Brandthovda" },
 new Area() { AreaName = "Bäckby" },
 new Area() { AreaName = "Haga" },
 new Area() { AreaName = "Hemdal" },
 new Area() { AreaName = "Hammarby" },
 new Area() { AreaName = "Malmaberg" },
 new Area() { AreaName = "Råby" },
 new Area() { AreaName = "Vallby" },
 new Area() { AreaName = "Viksäng" },
 new Area() { AreaName = "Centrum" },
 new Area() { AreaName = "Öster mälarstrand" },
 new Area() { AreaName = "Skallberget" },
  new Area() { AreaName = "Gideonsberg" },
    new Area() { AreaName = "Skiljebo" }
 );
        }

        public static void SeedPokemon(SightingsContext context)
        {
            context.Pokemons.AddOrUpdate(p => p.PokemonName,
                  new Pokemon() { PokemonName = "Bulbasaur" },
  new Pokemon() { PokemonName = "Ivysaur" },
  new Pokemon() { PokemonName = "Venusaur" },
  new Pokemon() { PokemonName = "Charmander" },
  new Pokemon() { PokemonName = "Charmeleon" },
  new Pokemon() { PokemonName = "Charizard" },
  new Pokemon() { PokemonName = "Squirtle" },
  new Pokemon() { PokemonName = "Wartortle" },
  new Pokemon() { PokemonName = "Blastoise" },
  new Pokemon() { PokemonName = "Caterpie" },
  new Pokemon() { PokemonName = "Metapod" },
  new Pokemon() { PokemonName = "Butterfree" },
  new Pokemon() { PokemonName = "Weedle" },
  new Pokemon() { PokemonName = "Kakuna" },
  new Pokemon() { PokemonName = "Beedrill" },
  new Pokemon() { PokemonName = "Pidgey" },
  new Pokemon() { PokemonName = "Pidgeotto" },
  new Pokemon() { PokemonName = "Pidgeot" },
  new Pokemon() { PokemonName = "Rattata" },
  new Pokemon() { PokemonName = "Raticate" },
  new Pokemon() { PokemonName = "Spearow" },
  new Pokemon() { PokemonName = "Fearow" },
  new Pokemon() { PokemonName = "Ekans" },
  new Pokemon() { PokemonName = "Arbok" },
  new Pokemon() { PokemonName = "Pikachu" },
  new Pokemon() { PokemonName = "Raichu" },
  new Pokemon() { PokemonName = "Sandshrew" },
  new Pokemon() { PokemonName = "Sandslash" },
  new Pokemon() { PokemonName = "Nidoran♀" },
  new Pokemon() { PokemonName = "Nidorina" },
  new Pokemon() { PokemonName = "Nidoqueen" },
  new Pokemon() { PokemonName = "Nidoran♂" },
  new Pokemon() { PokemonName = "Nidorino" },
  new Pokemon() { PokemonName = "Nidoking" },
  new Pokemon() { PokemonName = "Clefairy" },
  new Pokemon() { PokemonName = "Clefable" },
  new Pokemon() { PokemonName = "Vulpix" },
  new Pokemon() { PokemonName = "Ninetales" },
  new Pokemon() { PokemonName = "Jigglypuff" },
  new Pokemon() { PokemonName = "Wigglytuff" },
  new Pokemon() { PokemonName = "Zubat" },
  new Pokemon() { PokemonName = "Golbat" },
  new Pokemon() { PokemonName = "Oddish" },
  new Pokemon() { PokemonName = "Gloom" },
  new Pokemon() { PokemonName = "Vileplume" },
  new Pokemon() { PokemonName = "Paras" },
  new Pokemon() { PokemonName = "Parasect" },
  new Pokemon() { PokemonName = "Venonat" },
  new Pokemon() { PokemonName = "Venomoth" },
  new Pokemon() { PokemonName = "Diglett" },
  new Pokemon() { PokemonName = "Dugtrio" },
  new Pokemon() { PokemonName = "Meowth" },
  new Pokemon() { PokemonName = "Persian" },
  new Pokemon() { PokemonName = "Psyduck" },
  new Pokemon() { PokemonName = "Golduck" },
  new Pokemon() { PokemonName = "Mankey" },
  new Pokemon() { PokemonName = "Primeape" },
  new Pokemon() { PokemonName = "Growlithe" },
  new Pokemon() { PokemonName = "Arcanine" },
  new Pokemon() { PokemonName = "Poliwag" },
  new Pokemon() { PokemonName = "Poliwhirl" },
  new Pokemon() { PokemonName = "Poliwrath" },
  new Pokemon() { PokemonName = "Abra" },
  new Pokemon() { PokemonName = "Kadabra" },
  new Pokemon() { PokemonName = "Alakazam" },
  new Pokemon() { PokemonName = "Machop" },
  new Pokemon() { PokemonName = "Machoke" },
  new Pokemon() { PokemonName = "Machamp" },
  new Pokemon() { PokemonName = "Bellsprout" },
  new Pokemon() { PokemonName = "Weepinbell" },
  new Pokemon() { PokemonName = "Victreebel" },
  new Pokemon() { PokemonName = "Tentacool" },
  new Pokemon() { PokemonName = "Tentacruel" },
  new Pokemon() { PokemonName = "Geodude" },
  new Pokemon() { PokemonName = "Graveler" },
  new Pokemon() { PokemonName = "Golem" },
  new Pokemon() { PokemonName = "Ponyta" },
  new Pokemon() { PokemonName = "Rapidash" },
  new Pokemon() { PokemonName = "Slowpoke" },
  new Pokemon() { PokemonName = "Slowbro" },
  new Pokemon() { PokemonName = "Magnemite" },
  new Pokemon() { PokemonName = "Magneton" },
  new Pokemon() { PokemonName = "Farfetch'd" },
  new Pokemon() { PokemonName = "Doduo" },
  new Pokemon() { PokemonName = "Dodrio" },
  new Pokemon() { PokemonName = "Seel" },
  new Pokemon() { PokemonName = "Dewgong" },
  new Pokemon() { PokemonName = "Grimer" },
  new Pokemon() { PokemonName = "Muk" },
  new Pokemon() { PokemonName = "Shellder" },
  new Pokemon() { PokemonName = "Cloyster" },
  new Pokemon() { PokemonName = "Gastly" },
  new Pokemon() { PokemonName = "Haunter" },
  new Pokemon() { PokemonName = "Gengar" },
  new Pokemon() { PokemonName = "Onix" },
  new Pokemon() { PokemonName = "Drowzee" },
  new Pokemon() { PokemonName = "Hypno" },
  new Pokemon() { PokemonName = "Krabby" },
  new Pokemon() { PokemonName = "Kingler" },
  new Pokemon() { PokemonName = "Voltorb" },
  new Pokemon() { PokemonName = "Electrode" },
  new Pokemon() { PokemonName = "Exeggcute" },
  new Pokemon() { PokemonName = "Exeggutor" },
  new Pokemon() { PokemonName = "Cubone" },
  new Pokemon() { PokemonName = "Marowak" },
  new Pokemon() { PokemonName = "Hitmonlee" },
  new Pokemon() { PokemonName = "Hitmonchan" },
  new Pokemon() { PokemonName = "Lickitung" },
  new Pokemon() { PokemonName = "Koffing" },
  new Pokemon() { PokemonName = "Weezing" },
  new Pokemon() { PokemonName = "Rhyhorn" },
  new Pokemon() { PokemonName = "Rhydon" },
  new Pokemon() { PokemonName = "Chansey" },
  new Pokemon() { PokemonName = "Tangela" },
  new Pokemon() { PokemonName = "Kangaskhan" },
  new Pokemon() { PokemonName = "Horsea" },
  new Pokemon() { PokemonName = "Seadra" },
  new Pokemon() { PokemonName = "Goldeen" },
  new Pokemon() { PokemonName = "Seaking" },
  new Pokemon() { PokemonName = "Staryu" },
  new Pokemon() { PokemonName = "Starmie" },
  new Pokemon() { PokemonName = "Mr. Mime" },
  new Pokemon() { PokemonName = "Scyther" },
  new Pokemon() { PokemonName = "Jynx" },
  new Pokemon() { PokemonName = "Electabuzz" },
  new Pokemon() { PokemonName = "Magmar" },
  new Pokemon() { PokemonName = "Pinsir" },
  new Pokemon() { PokemonName = "Tauros" },
  new Pokemon() { PokemonName = "Magikarp" },
  new Pokemon() { PokemonName = "Gyarados" },
  new Pokemon() { PokemonName = "Lapras" },
  new Pokemon() { PokemonName = "Ditto" },
  new Pokemon() { PokemonName = "Eevee" },
  new Pokemon() { PokemonName = "Vaporeon" },
  new Pokemon() { PokemonName = "Jolteon" },
  new Pokemon() { PokemonName = "Flareon" },
  new Pokemon() { PokemonName = "Porygon" },
  new Pokemon() { PokemonName = "Omanyte" },
  new Pokemon() { PokemonName = "Omastar" },
  new Pokemon() { PokemonName = "Kabuto" },
  new Pokemon() { PokemonName = "Kabutops" },
  new Pokemon() { PokemonName = "Aerodactyl" },
  new Pokemon() { PokemonName = "Snorlax" },
  new Pokemon() { PokemonName = "Articuno" },
  new Pokemon() { PokemonName = "Zapdos" },
  new Pokemon() { PokemonName = "Moltres" },
  new Pokemon() { PokemonName = "Dratini" },
  new Pokemon() { PokemonName = "Dragonair" },
  new Pokemon() { PokemonName = "Dragonite" },
  new Pokemon() { PokemonName = "Mewtwo" },
  new Pokemon() { PokemonName = "Mew" }
                );
        }
    }
}

