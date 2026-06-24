import React from "react";
import { useAuth } from "../hooks/useAuth";
import { Navigate } from "react-router-dom";

/**
 * Checks if the user is logged in before allowing access.
 */
const PrivateRoute = ({
  children,
  allowdRoles,
}: {
  children: React.ReactNode;
  allowdRoles?: string[];
}) => {
  const { token, loading, user } = useAuth();

  if (loading) return null;
  if (!user) return <Navigate to="/login" replace />;

  if (allowdRoles && !allowdRoles.includes(token?.role?.toLowerCase() || ""))
    return <Navigate to="/" replace />;

  return <>{children}</>;
};

export default PrivateRoute;
