package org.fegaeze;

public class Main {
    public static void main(String[] args) {
        Player mary = new Player();
        mary.setName("Mary");
        mary.setTurn(true);
        Player james = new Player();
        james.setName("James");
        james.setTurn(false);
        GameSystem gameSystem = new GameSystem();
        gameSystem.setDisplay("Mary's turn");
        gameSystem.setGameOver(false);
        Board board = new Board();
        PlayingPit p1 = new PlayingPit();
        PlayingPit p2 = new PlayingPit();
        PlayingPit p3 = new PlayingPit();
        PlayingPit p4 = new PlayingPit();
        PlayingPit p5 = new PlayingPit();
        PlayingPit p6 = new PlayingPit();
        PlayingPit p7 = new PlayingPit();
        PlayingPit p8 = new PlayingPit();
        PlayingPit p9 = new PlayingPit();
        PlayingPit p10 = new PlayingPit();
        PlayingPit p11 = new PlayingPit();
        PlayingPit p12 = new PlayingPit();
        p1.setNames("p1");
        p2.setNames("p2");
        p3.setNames("p3");
        p4.setNames("p4");
        p5.setNames("p5");
        p6.setNames("p6");
        p7.setNames("p7");
        p8.setNames("p8");
        p9.setNames("p9");
        p10.setNames("p10");
        p11.setNames("p11");
        p12.setNames("p12");
        p1.setNumberOfStones(3);
        p2.setNumberOfStones(3);
        p3.setNumberOfStones(3);
        p4.setNumberOfStones(3);
        p5.setNumberOfStones(3);
        p6.setNumberOfStones(3);
        p7.setNumberOfStones(3);
        p8.setNumberOfStones(3);
        p9.setNumberOfStones(3);
        p10.setNumberOfStones(3);
        p11.setNumberOfStones(3);
        p12.setNumberOfStones(3);
        Kalah maryKalah = new Kalah();
        Kalah jamesKalah = new Kalah();
        maryKalah.setNames("MaryKalah");
        jamesKalah.setNames("JamesKalah");
        maryKalah.setNumberOfStones(0);
        jamesKalah.setNumberOfStones(0);
        gameSystem.withPlayers(mary, james);
        board.withPlayingPits(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12);
        board.withKalah(maryKalah, jamesKalah);
        gameSystem.setBoard(board);
    }
}
