package HuntTheWumpus;

import static HuntTheWumpus.Game.*;
import HuntTheWumpus.fixtures.MockConsole;
import org.junit.Before;
import org.junit.Test;
import static org.junit.Assert.*;

public class GamePresenterTest {
  private GamePresenter p;
  private MockConsole mc;
  private Game g;

  @Before
  public void initialize() throws Exception {
    mc = new MockConsole();
    p = new GamePresenter(mc);
    g = p.getGame();
  }

  @Test
  public void generatesCorrectDirectionMessage() throws Exception {
    g.putPlayerInCavern(1);
    p.execute(EAST);
    assertTrue(mc.check("You can't go east from here."));
    p.execute(WEST);
    assertTrue(mc.check("You can't go west from here."));
    p.execute(SOUTH);
    assertTrue(mc.check("You can't go south from here."));
    p.execute(NORTH);
    assertTrue(mc.check("You can't go north from here."));
  }

  @Test
  public void canDetectThatThereAreNoExits() {
    g.putPlayerInCavern(1);
    assertEquals("There are no exits!", p.getAvailableDirections());
  }

  @Test
  public void generatesSouthDirectionMessage() throws Exception {
    g.addPath(1, 2, SOUTH);
    g.putPlayerInCavern(1);
    assertEquals("You can go south from here.", p.getAvailableDirections());
  }

  @Test
  public void generatesNorthAndSouthDirectionMessage() throws Exception {
    g.addPath(1, 2, SOUTH);
    g.addPath(1, 3, NORTH);
    g.putPlayerInCavern(1);
    assertEquals("You can go north and south from here.", p.getAvailableDirections());
  }

  @Test
  public void generatesMessageWithAllFourDirections() throws Exception {
    g.addPath(1, 2, EAST);
    g.addPath(1, 3, WEST);
    g.addPath(1, 4, SOUTH);
    g.addPath(1, 5, NORTH);
    g.putPlayerInCavern(1);
    assertEquals("You can go north, south, east and west from here.", p.getAvailableDirections());
  }
}
