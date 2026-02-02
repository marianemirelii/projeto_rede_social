import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import type { AppDispatch, RootState } from "../../store";
import {
  loadFriendRequests,
  answerFriendRequest,
} from "../../store/friendsSlice";
import "./FriendRequests.css";

export function FriendRequests() {
  const dispatch = useDispatch<AppDispatch>();

  const { requests, requestsLoading } = useSelector(
    (state: RootState) => state.friends
  );

  useEffect(() => {
    dispatch(loadFriendRequests());
  }, [dispatch]);

  if (requestsLoading) {
    return <p className="requests-empty">Carregando solicitações...</p>;
  }

  if (!requests.length) {
    return <p className="requests-empty">Nenhuma solicitação pendente</p>;
  }

  return (
    <section className="requests-container">
      {requests.map(item => (
        <div
          key={item.friend.idFriend}
          className="request-item"
        >
          <img
            src={
              item.friend.imageUrl ??
              "https://voxnews.com.br/wp-content/uploads/2017/04/unnamed.png"
            }
            alt={item.friend.friendName}
          />

          <div className="request-info">
            <strong>{item.friend.friendName}</strong>
            <span>{item.friend.friendEmail}</span>
          </div>

          <div className="request-actions">
            <button
              className="accept"
              onClick={() =>
                dispatch(
                  answerFriendRequest({
                    friendId: item.friend.idFriend,
                    status: 2,
                  })
                )
              }
            >
              Aceitar
            </button>

            <button
              className="reject"
              onClick={() =>
                dispatch(
                  answerFriendRequest({
                    friendId: item.friend.idFriend,
                    status: 3,
                  })
                )
              }
            >
              Recusar
            </button>
          </div>
        </div>
      ))}
    </section>
  );
}
