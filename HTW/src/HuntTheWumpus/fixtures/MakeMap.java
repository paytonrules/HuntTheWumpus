package HuntTheWumpus.fixtures;

import fit.*;

public class MakeMap extends ColumnFixture {
  public int start;
  public int end;
  public String direction;

  public void execute() throws Exception {
    GameDriver.p.getGame().addPath(start, end, direction);
  }
}
