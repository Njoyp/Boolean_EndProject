import {Outlet, Link } from "react-router-dom";
import '../Styling/Menu.css'
function Menu() {
    return (
        <>
            <div className="topMenu">
            <h3>Menu</h3>
            <nav>
                <ul>
                    <li>
                    <Link className="menuLink" to="/">Home</Link>
                    </li>
                    <li>
                            <Link className="menuLink" to="/allrecipes">All recipes</Link>
                    </li>
                    <li>
                            <Link className="menuLink"  to="/selectedrecipes">Selecte recipes</Link>
                    </li>
                    <li>
                            <Link className="menuLink"  to="/shoppinglist">Shopping list</Link>
                    </li>
                </ul>
                </nav>
            </div>
            <Outlet/>
        </>
    )
}
export default Menu;