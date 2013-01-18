package HuntTheWumpus;

import static HuntTheWumpus.Game.EAST;
import static HuntTheWumpus.Game.WEST;
import HuntTheWumpus.fixtures.MockConsole;
import static org.junit.Assert.*;
import org.junit.Before;
import org.junit.Test;

public class GameTest {
  private Game g;
  private MockConsole mc;

  @Before
  public void initialize() throws Exception {
    g = new Game();
  }

  @Test
  public void canGoEast() throws Exception {
    g.addPath(1, 2, EAST);
    g.putPlayerInCavern(1);
    g.move(EAST);
    assertEquals(2, g.playerCavern());
  }

  @Test
  public void canGoEastTwice() throws Exception {
    g.addPath(1, 2, EAST);
    g.addPath(2, 3, EAST);
    g.putPlayerInCavern(1);
    g.move(EAST);
    g.move(EAST);
    assertEquals(3, g.playerCavern());
  }

  @Test
  public void canSmellWumpus() throws Exception {
    g.addPath(1, 2, EAST);
    g.putPlayerInCavern(1);
    g.putWumpusInCavern(2);
    assertTrue(g.canSmellWumpus());
  }

  @Test
  public void canNotSmellWumpus() throws Exception {
    g.addPath(1, 2, EAST);
    g.addPath(2, 3, EAST);
    g.putPlayerInCavern(1);
    g.putWumpusInCavern(3);
    assertFalse(g.canSmellWumpus());
  }

  @Test
  public void canPickUpArrows() throws Exception {
    g.addPath(1, 2, EAST);
    g.putPlayerInCavern(1);
    g.setQuiver(0);
    g.putArrowInCavern(2);
    g.move(EAST);
    assertEquals(1, g.getQuiver());
    assertEquals(0, g.arrowsInCavern(2));
  }

  @Test
  public void shootArrow() throws Exception {
    g.addPath(1, 2, EAST);
    g.addPath(2, 3, EAST);
    g.putPlayerInCavern(1);
    g.setQuiver(1);
    assertTrue(g.shoot(EAST));
    assertEquals(0, g.getQuiver());
    assertEquals(1, g.arrowsInCavern(3));
    assertEquals(0, g.arrowsInCavern(2));
    assertFalse(g.gameTerminated());
  }

  @Test
  public void shootArrowWhenQuiverEmpty() throws Exception {
    g.addPath(1, 2, EAST);
    g.putPlayerInCavern(1);
    g.setQuiver(0);
    assertFalse(g.shoot(EAST));
    assertEquals(0, g.getQuiver());
    assertEquals(0, g.arrowsInCavern(2));
  }

  @Test
  public void playerDiesIfShootsAtWall() throws Exception {
    g.putPlayerInCavern(1);
    g.setQuiver(1);
    g.shoot(EAST);
    assertTrue(g.gameTerminated());
  }

  @Test
  public void fallInPit() throws Exception {
    g.addPath(1, 2, EAST);
    g.putPlayerInCavern(1);
    g.putPitInCavern(2);
    g.move(EAST);
    assertTrue(g.gameTerminated());
    assertTrue(g.fellInPit());
  }

  @Test
  public void hearPit() throws Exception {
    g.addPath(1, 2, EAST);
    g.addPath(2, 3, EAST);
    g.putPlayerInCavern(1);
    g.putPitInCavern(3);
    g.move(EAST);
    assertTrue(g.canHearPit());
    g.move(WEST);
    assertFalse(g.canHearPit());
  }

  @Test
  public void killWumpusAtDistance() throws Exception {
    g.addPath(1, 2, EAST);
    g.addPath(2, 3, EAST);
    g.putWumpusInCavern(3);
    g.putPlayerInCavern(1);
    g.setQuiver(1);
    g.shoot(EAST);
    assertTrue(g.wumpusHitByArrow());
    assertTrue(g.gameTerminated());
  }

  @Test
  public void killWumpusUpClose() throws Exception {
    g.addPath(1, 2, EAST);
    g.putWumpusInCavern(2);
    g.putPlayerInCavern(1);
    g.setQuiver(1);
    g.shoot(EAST);
    assertTrue(g.wumpusHitByArrow());
    assertTrue(g.gameTerminated());
  }

  @Test
  public void randomWumpusMovement() throws Exception {
    boolean moved = false;
    g.addPath(1, 2, EAST);
    g.putWumpusInCavern(1);
    for (int i = 0; i < 100; i++) {
      g.moveWumpus();
      if (g.getWumpusCavern() == 2) {
        moved = true;
        break;
      }
    }
    assertTrue(moved);
  }

  @Test
  public void hearBats() throws Exception {
    g.addPath(1, 2, EAST);
    g.putPlayerInCavern(1);
    g.putBatsInCavern(2);
    assertTrue(g.canHearBats());
  }

  @Test
  public void batsCarryYouAway() throws Exception {
    g.addPath(1, 2, EAST);
    g.putPlayerInCavern(1);
    g.putBatsInCavern(2);
    g.move(EAST);
    assertTrue(g.batTransport());
    assertEquals(1, g.playerCavern());
  }
}
