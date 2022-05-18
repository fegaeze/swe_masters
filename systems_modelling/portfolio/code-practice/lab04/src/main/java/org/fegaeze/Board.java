package org.fegaeze;
import java.util.ArrayList;
import java.util.List;
import java.util.Collections;
import java.util.Collection;
import java.beans.PropertyChangeSupport;

public class Board
{
   public static final String PROPERTY_GAME_SYSTEM = "gameSystem";
   public static final String PROPERTY_PLAYING_PITS = "playingPits";
   public static final String PROPERTY_KALAH = "kalah";
   private GameSystem gameSystem;
   private List<PlayingPit> playingPits;
   private List<Kalah> kalah;
   protected PropertyChangeSupport listeners;

   public GameSystem getGameSystem()
   {
      return this.gameSystem;
   }

   public Board setGameSystem(GameSystem value)
   {
      if (this.gameSystem == value)
      {
         return this;
      }

      final GameSystem oldValue = this.gameSystem;
      if (this.gameSystem != null)
      {
         this.gameSystem = null;
         oldValue.setBoard(null);
      }
      this.gameSystem = value;
      if (value != null)
      {
         value.setBoard(this);
      }
      this.firePropertyChange(PROPERTY_GAME_SYSTEM, oldValue, value);
      return this;
   }

   public List<PlayingPit> getPlayingPits()
   {
      return this.playingPits != null ? Collections.unmodifiableList(this.playingPits) : Collections.emptyList();
   }

   public Board withPlayingPits(PlayingPit value)
   {
      if (this.playingPits == null)
      {
         this.playingPits = new ArrayList<>();
      }
      if (!this.playingPits.contains(value))
      {
         this.playingPits.add(value);
         value.setBoard(this);
         this.firePropertyChange(PROPERTY_PLAYING_PITS, null, value);
      }
      return this;
   }

   public Board withPlayingPits(PlayingPit... value)
   {
      for (final PlayingPit item : value)
      {
         this.withPlayingPits(item);
      }
      return this;
   }

   public Board withPlayingPits(Collection<? extends PlayingPit> value)
   {
      for (final PlayingPit item : value)
      {
         this.withPlayingPits(item);
      }
      return this;
   }

   public Board withoutPlayingPits(PlayingPit value)
   {
      if (this.playingPits != null && this.playingPits.remove(value))
      {
         value.setBoard(null);
         this.firePropertyChange(PROPERTY_PLAYING_PITS, value, null);
      }
      return this;
   }

   public Board withoutPlayingPits(PlayingPit... value)
   {
      for (final PlayingPit item : value)
      {
         this.withoutPlayingPits(item);
      }
      return this;
   }

   public Board withoutPlayingPits(Collection<? extends PlayingPit> value)
   {
      for (final PlayingPit item : value)
      {
         this.withoutPlayingPits(item);
      }
      return this;
   }

   public List<Kalah> getKalah()
   {
      return this.kalah != null ? Collections.unmodifiableList(this.kalah) : Collections.emptyList();
   }

   public Board withKalah(Kalah value)
   {
      if (this.kalah == null)
      {
         this.kalah = new ArrayList<>();
      }
      if (!this.kalah.contains(value))
      {
         this.kalah.add(value);
         value.setBoard(this);
         this.firePropertyChange(PROPERTY_KALAH, null, value);
      }
      return this;
   }

   public Board withKalah(Kalah... value)
   {
      for (final Kalah item : value)
      {
         this.withKalah(item);
      }
      return this;
   }

   public Board withKalah(Collection<? extends Kalah> value)
   {
      for (final Kalah item : value)
      {
         this.withKalah(item);
      }
      return this;
   }

   public Board withoutKalah(Kalah value)
   {
      if (this.kalah != null && this.kalah.remove(value))
      {
         value.setBoard(null);
         this.firePropertyChange(PROPERTY_KALAH, value, null);
      }
      return this;
   }

   public Board withoutKalah(Kalah... value)
   {
      for (final Kalah item : value)
      {
         this.withoutKalah(item);
      }
      return this;
   }

   public Board withoutKalah(Collection<? extends Kalah> value)
   {
      for (final Kalah item : value)
      {
         this.withoutKalah(item);
      }
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

   public void removeYou()
   {
      this.setGameSystem(null);
      this.withoutPlayingPits(new ArrayList<>(this.getPlayingPits()));
      this.withoutKalah(new ArrayList<>(this.getKalah()));
   }
}
