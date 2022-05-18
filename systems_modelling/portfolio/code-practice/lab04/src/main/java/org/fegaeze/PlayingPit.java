package org.fegaeze;
import java.util.Objects;
import java.beans.PropertyChangeSupport;

public class PlayingPit
{
   public static final String PROPERTY_NAMES = "names";
   public static final String PROPERTY_NUMBER_OF_STONES = "numberOfStones";
   public static final String PROPERTY_BOARD = "board";
   private String names;
   private int numberOfStones;
   private Board board;
   protected PropertyChangeSupport listeners;

   public String getNames()
   {
      return this.names;
   }

   public PlayingPit setNames(String value)
   {
      if (Objects.equals(value, this.names))
      {
         return this;
      }

      final String oldValue = this.names;
      this.names = value;
      this.firePropertyChange(PROPERTY_NAMES, oldValue, value);
      return this;
   }

   public int getNumberOfStones()
   {
      return this.numberOfStones;
   }

   public PlayingPit setNumberOfStones(int value)
   {
      if (value == this.numberOfStones)
      {
         return this;
      }

      final int oldValue = this.numberOfStones;
      this.numberOfStones = value;
      this.firePropertyChange(PROPERTY_NUMBER_OF_STONES, oldValue, value);
      return this;
   }

   public Board getBoard()
   {
      return this.board;
   }

   public PlayingPit setBoard(Board value)
   {
      if (this.board == value)
      {
         return this;
      }

      final Board oldValue = this.board;
      if (this.board != null)
      {
         this.board = null;
         oldValue.withoutPlayingPits(this);
      }
      this.board = value;
      if (value != null)
      {
         value.withPlayingPits(this);
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
      result.append(' ').append(this.getNames());
      return result.substring(1);
   }

   public void removeYou()
   {
      this.setBoard(null);
   }
}
