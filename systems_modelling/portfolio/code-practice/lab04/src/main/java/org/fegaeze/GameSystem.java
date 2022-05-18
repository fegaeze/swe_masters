package org.fegaeze;
import java.util.ArrayList;
import java.util.List;
import java.util.Objects;
import java.util.Collections;
import java.util.Collection;
import java.beans.PropertyChangeSupport;

public class GameSystem
{
   public static final String PROPERTY_DISPLAY = "display";
   public static final String PROPERTY_GAME_OVER = "gameOver";
   public static final String PROPERTY_PLAYERS = "players";
   public static final String PROPERTY_BOARD = "board";
   private String display;
   private boolean gameOver;
   private List<Player> players;
   private Board board;
   protected PropertyChangeSupport listeners;

   public String getDisplay()
   {
      return this.display;
   }

   public GameSystem setDisplay(String value)
   {
      if (Objects.equals(value, this.display))
      {
         return this;
      }

      final String oldValue = this.display;
      this.display = value;
      this.firePropertyChange(PROPERTY_DISPLAY, oldValue, value);
      return this;
   }

   public boolean isGameOver()
   {
      return this.gameOver;
   }

   public GameSystem setGameOver(boolean value)
   {
      if (value == this.gameOver)
      {
         return this;
      }

      final boolean oldValue = this.gameOver;
      this.gameOver = value;
      this.firePropertyChange(PROPERTY_GAME_OVER, oldValue, value);
      return this;
   }

   public List<Player> getPlayers()
   {
      return this.players != null ? Collections.unmodifiableList(this.players) : Collections.emptyList();
   }

   public GameSystem withPlayers(Player value)
   {
      if (this.players == null)
      {
         this.players = new ArrayList<>();
      }
      if (!this.players.contains(value))
      {
         this.players.add(value);
         value.setGamesystem(this);
         this.firePropertyChange(PROPERTY_PLAYERS, null, value);
      }
      return this;
   }

   public GameSystem withPlayers(Player... value)
   {
      for (final Player item : value)
      {
         this.withPlayers(item);
      }
      return this;
   }

   public GameSystem withPlayers(Collection<? extends Player> value)
   {
      for (final Player item : value)
      {
         this.withPlayers(item);
      }
      return this;
   }

   public GameSystem withoutPlayers(Player value)
   {
      if (this.players != null && this.players.remove(value))
      {
         value.setGamesystem(null);
         this.firePropertyChange(PROPERTY_PLAYERS, value, null);
      }
      return this;
   }

   public GameSystem withoutPlayers(Player... value)
   {
      for (final Player item : value)
      {
         this.withoutPlayers(item);
      }
      return this;
   }

   public GameSystem withoutPlayers(Collection<? extends Player> value)
   {
      for (final Player item : value)
      {
         this.withoutPlayers(item);
      }
      return this;
   }

   public Board getBoard()
   {
      return this.board;
   }

   public GameSystem setBoard(Board value)
   {
      if (this.board == value)
      {
         return this;
      }

      final Board oldValue = this.board;
      if (this.board != null)
      {
         this.board = null;
         oldValue.setGameSystem(null);
      }
      this.board = value;
      if (value != null)
      {
         value.setGameSystem(this);
      }
      this.firePropertyChange(PROPERTY_BOARD, oldValue, value);
      return this;
   }

   public boolean firePropertyChange(String propertyName, Object oldValue, Object newValue)
   {
      if (this.listeners != null)
      {
         this.listeners.firePropertyChange(propertyName, oldValue, newValue);
         return true;
      }
      return false;
   }

   public PropertyChangeSupport listeners()
   {
      if (this.listeners == null)
      {
         this.listeners = new PropertyChangeSupport(this);
      }
      return this.listeners;
   }

   @Override
   public String toString()
   {
      final StringBuilder result = new StringBuilder();
      result.append(' ').append(this.getDisplay());
      return result.substring(1);
   }

   public void removeYou()
   {
      this.withoutPlayers(new ArrayList<>(this.getPlayers()));
      this.setBoard(null);
   }
}
