using System.Collections.Generic;

//The pathfinding algorithm should be implemented by the developer (built-in Unity
//solutions or external packages should not be used) and the system should be robust
//enough so the extension of it and adding different algorithms is simple.

//NE ZNAM STA OVO ZNACI - sve sto ima interface ili abstrac je manje vise lako za prosirivanje
//u ovom slucaju treba da se implementira drugi algoritam u klasa koja nasledjuje ISearchAlgorithm interface (koji uopste nije generic, ali sta sad... to je specificno za ovaj projekat...)
//da se umesto AStar instancira ta druga klasa i to je to...
//eventualno da se napravi ScriptableObject koji ce nasledivati ovaj interface, pa da se prosledjuje kroz editor
//mada opet.. ja ne volim da guram kroz editor jer malo nesto cacnes promenljivu i sve reference se pogube
//kroz kod mi je sigurica, bar meni... kontam da u timovima gde ima i drugih bransi se pravi da ide preko editora...
public interface ISearchAlgorithm
{
    IEnumerable<NodeController> GetRoute();
}
