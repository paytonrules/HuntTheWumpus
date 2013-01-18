package HuntTheWumpus.fixtures;

import static HuntTheWumpus.fixtures.GameDriver.*;
import fit.*;

public class CheckRandomWumpusMovement extends ColumnFixture {
  private int carvernCounts[] = new int[6];
  public int cavern;

  public void doTable(Parse parse) {
    int wumpusCavern = 2;
    for (int i = 0; i < 1000; i++) {
      g.putWumpusInCavern(wumpusCavern);
      p.execute("R");
      carvernCounts[g.getWumpusCavern()]++;
    }
    super.doTable(parse);
  }

  public int count() {
    return carvernCounts[cavern];
  }
}
