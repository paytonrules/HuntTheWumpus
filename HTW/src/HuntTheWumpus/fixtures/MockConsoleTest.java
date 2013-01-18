package HuntTheWumpus.fixtures;

import static org.junit.Assert.*;
import org.junit.Before;
import org.junit.Test;

public class MockConsoleTest{
  private MockConsole mc;

  @Before
  public void initialize() throws Exception {
    mc = new MockConsole();
  }

  @Test
  public void checkFailsOnNoMessage() throws Exception {
    assertFalse(mc.check("message"));
  }


  @Test
  public void checkPassesWithOneMessage() {
    mc.print("message");
    assertTrue(mc.check("message"));
  }

  @Test
  public void checkPassesWithThreeMessages() {
    mc.print("message one");
    mc.print("message two");
    mc.print("message three");
    assertTrue(mc.check("message three"));
    assertTrue(mc.check("message one"));
    assertTrue(mc.check("message two"));
    assertFalse(mc.check("message x"));
    assertFalse(mc.check("message"));
  }

  @Test
  public void listClearedBetweenCheckAndPrint() {
    mc.print("message one");
    assertTrue(mc.check("message one"));
    mc.print("message two");
    assertFalse(mc.check("message one"));
    assertTrue(mc.check("message two"));
  }
}
