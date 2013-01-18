package HuntTheWumpus.fixtures;

import fit.*;
import static HuntTheWumpus.fixtures.GameDriver.*;
import static HuntTheWumpus.Game.*;

public class CheckRandomBatTransport extends ColumnFixture {
  public int cavern;
  public int count() {
    return counts[cavern];
  }

  private int counts[] = new int[6];

  public void doTable(Parse parse) {
    g.putBatsInCavern(2);

    for (int i=0; i<1000; i++) {
      g.putPlayerInCavern(1);
      g.move(EAST);
      counts[g.playerCavern()]++;
    }
    super.doTable(parse);
  }
}
