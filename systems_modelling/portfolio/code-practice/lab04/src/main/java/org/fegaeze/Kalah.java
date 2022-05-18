package org.fegaeze;
import java.util.Objects;
import java.beans.PropertyChangeSupport;

public class Kalah
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

   public Kalah setNames(String value)
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

   public Kalah setNumberOfStones(int value)
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

   public Kalah setBoard(Board value)
   {
      if (this.board == value)
      {
         return this;
      }

      final Board oldValue = this.board;
      if (this.board != null)
      {
         this.board = null;
         oldValue.withoutKalah(this);
      }
      this.board = value;
      if (value != null)
      {
         value.withKalah(this);
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
