defmodule BookYourTaxi.Authentication do
  import Plug.Conn

  def init(opts) do
    opts[:repo]
  end

  def call(conn, repo) do
    user_id = get_session(conn, :user_id)
    user = user_id && repo.get(BookYourTaxi.Accounts.User, user_id)
    login(conn, user_id, user)
  end

  def login(conn, user_id, user) do
    assign(conn, :current_user, user)
    |> put_session(:user_id, user_id)
  end

  def logout(conn) do
    configure_session(conn, drop: true)
  end

  def check_credentials(conn, email, password, [repo: repo]) do
    user = repo.get_by(BookYourTaxi.Accounts.User, email: email)

    if user != nil && user.password == password do
      {:ok, login(conn, user.id, user) }
    else
      {:error, :unauthorized, conn}
    end
  end
end
