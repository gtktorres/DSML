import './Main.css';

function Main() {
    return(
        <div class="vh-100 px-4 py-5 my-5 text-center">
            <h1 class="display-5 fw-bold text-body-emphasis">DSML</h1>
            <div class="col-lg-6 mx-auto">
            <p class="lead mb-4">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam ultrices venenatis feugiat. Nam auctor felis id urna varius auctor. Curabitur commodo ultrices feugiat. Vestibulum non ultrices tellus, id pretium risus. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Pellentesque congue ullamcorper libero non malesuada.</p>
            <div class="d-grid gap-2 d-sm-flex justify-content-sm-center">
                <button type="button" class="btn btn-primary btn-lg px-4 gap-3">Primary button</button>
                <button type="button" class="btn btn-outline-secondary btn-lg px-4">Secondary</button>
            </div>
            </div>
        </div>
    );
}

export default Main;