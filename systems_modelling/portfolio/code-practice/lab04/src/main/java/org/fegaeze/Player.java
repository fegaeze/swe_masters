package org.fegaeze;
import java.util.Objects;
import java.beans.PropertyChangeSupport;

public class Player
{
   public static final String PROPERTY_NAME = "name";
   public static final String PROPERTY_TURN = "turn";
   public static final String PROPERTY_GAMESYSTEM = "gamesystem";
   private String name;
   private boolean turn;
   private GameSystem gamesystem;
   protected PropertyChangeSupport listeners;

   public String getName()
   {
      return this.name;
   }

   public Player setName(String value)
   {
      if (Objects.equals(value, this.name))
      {
         return this;
      }

      final String oldValue = this.name;
      this.name = value;
      this.firePropertyChange(PROPERTY_NAME, oldValue, value);
      return this;
   }

   public boolean isTurn()
   {
      return this.turn;
   }

   public Player setTurn(boolean value)
   {
      if (value == this.turn)
      {
         return this;
      }

      final boolean oldValue = this.turn;
      this.turn = value;
      this.firePropertyChange(PROPERTY_TURN, oldValue, value);
      return this;
   }

   public GameSystem getGamesystem()
   {
      return this.gamesystem;
   }

   public Player setGamesystem(GameSystem value)
   {
      if (this.gamesystem == value)
      {
         return this;
      }

      final GameSystem oldValue = this.gamesystem;
      if (this.gamesystem != null)
      {
         this.gamesystem = null;
         oldValue.withoutPlayers(this);
      }
      this.gamesystem = value;
      if (value != null)
      {
         value.withPlayers(this);
      }
      this.firePropertyChange(PROPERTY_GAMESYSTEM, oldValue, value);
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
      result.append(' ').append(this.getName());
      return result.substring(1);
   }

   public void removeYou()
   {
      this.setGamesystem(null);
   }
}
